namespace ATUWordle;

public partial class WelcomePage : ContentPage
{
    private int choice;

    public WelcomePage()
    {
        InitializeComponent();
    }

    private async void PlayBtn_Clicked(object sender, EventArgs e)
    {
        choice = 0;
        Preferences.Default.Set("DeviceWidth", this.Width);
        await Shell.Current.GoToAsync("//MainPage", true);
    }

    private async void btnHelp_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HelpPage());
    }
}
