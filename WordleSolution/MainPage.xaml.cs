using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ATUWordle
{
    public partial class MainPage : ContentPage
    {
        // 0 - 5 
        int rowIndex;
        // 0 - 4
        int columnIndex;
        char[] correctAnswer;
        public char[] KeyboardRow1 { get; }
        public char[] KeyboardRow2 { get; }
        public char[] KeyboardRow3 { get; }
        public WordRow[] Rows { get; }
        public ICommand EnterLetterCommand { get; protected set; }
        private int winnerOfGame = -1;
        private bool guessingwordle = false;
        private bool loadingpage = false;
        private List<Player> players;
        private int playerTurn;
        private bool submitword = false;
        private bool everythingInitialised = false;
        private int choice;

        public bool LoadingPage
        {
            get => loadingpage;
            set
            {
                loadingpage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EverythingLoaded));
            }
        }
        public bool EverythingLoaded => !LoadingPage;

        private bool GuessingWordle
        {
            get => guessingwordle;
            set
            {
                if (guessingwordle != value)
                {
                    guessingwordle = value;
                    OnPropertyChanged(nameof(TopText));
                }
            }
        }
        private int WinnerGame
        {
            get => winnerOfGame;
            set
            {
                if (winnerOfGame != value)
                {
                    winnerOfGame = value;
                    OnPropertyChanged(nameof(TopText));
                }
            }
        }
        private bool SubmitWord
        {
            get => submitword;
            set
            {
                if (submitword != value)
                {
                    submitword = value;
                    OnPropertyChanged(nameof(TopText));
                }
            }
        }
        public string TopText
        {
            get
            {
                if (players == null || players.Count == 0)
                    return "";
                if (WinnerGame != -1)
                    return "Well done " + players[playerTurn].name + " you have won!!\nIf you'd like to start again, click the New Game Button";
                else if (GuessingWordle)
                    return "Please Wait, Guessing Wordle!!";
                else if (SubmitWord)
                    return "Submitting the correct word, let's see what you get";
                else
                    return players[playerTurn].name + ", it's your go - Guess the correct word";
            }
        }
        private string correctword;
        public string CorrectWord
        {
            get { return correctword; }
            set { correctword = value; }
        }

        public MainPage()
        {
            InitializeComponent();
            this.LayoutChanged += OnWindowChange;
            EnterLetterCommand = new Command(async () => await EnterLetter('A'));

            Rows = new WordRow[6]
            {
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow()
            };

            correctAnswer = "CODES".ToCharArray();
            KeyboardRow1 = "QWERTYUIOP".ToCharArray();
            KeyboardRow2 = "ASDFGHJKL".ToCharArray();
            KeyboardRow3 = "⌫ZXCVBNM↵".ToCharArray();
        }

        private void OnWindowChange(object sender, EventArgs e)
        {
            if (Width <= 0)
                return;
            if (Width < 480)
            {
                int newdim = (int)Width / 10;
                double rescale = (double)newdim * 10 / 480;
                GridGameTable.Scale = rescale;
                TopTextLbl.WidthRequest = newdim * 10;
            }
        }

        public void Enter()
        {
            if (columnIndex != 5)
                App.Current.MainPage.DisplayAlert("Error", "Invalid Input!", "OK");

            var correct = Rows[rowIndex].Validate(correctAnswer);

            if (correct)
            {
                App.Current.MainPage.DisplayAlert("You Win!", "You are so smart!", "OK");
                return;
            }

            if (rowIndex == 5)
            {
                App.Current.MainPage.DisplayAlert("Game Over!", "You are out of turns", "OK");
            }
            else
            {
                rowIndex++;
                columnIndex = 0;
            }
        }

        public Task EnterLetter(char letter)
        {
            if (letter == '↵')
            {
                Enter();
                return Task.CompletedTask;
            }

            if (letter == '⌫')
            {
                if (columnIndex == 0)
                    return Task.CompletedTask;
                columnIndex--;
                Rows[rowIndex].Letters[columnIndex].Input = ' ';
                return Task.CompletedTask;
            }

            if (columnIndex == 5)
                return Task.CompletedTask;
            Rows[rowIndex].Letters[columnIndex].Input = letter;
            columnIndex++;
            return Task.CompletedTask;
        }

        private async void btnHelp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpPage());
        }

        private async void NewGameButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Are you sure", "Are you sure you want to start a new game?", "Yes", "No");
            if (!answer)
                return;
            await Shell.Current.GoToAsync("//WelcomePage", true);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!everythingInitialised)
            {
                LoadingPage = true;
                BindingContext = this;
                await Task.Delay(500);
                LoadingPage = false;
                WinnerGame = 0;
                WinnerGame = -1;
            }
        }
    }
}
