
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATUWordle
{
    public class ListofWords
    {
        Random random;
        public ListofWords(Random random)
        {
            this.random = random;
        }

        private List<string> words;
        public List<string> Words
        
        {
            get { return words; }
            set { words = value; }
        }
        private int lengthoflist;
        public int LengthOfList
        {
            get { return lengthoflist; }
            set { lengthoflist = value; }
        }
        public async void ReadFileIntoList()
        {
            string line;
            MainPage rPage = new MainPage();
            try
            {
                using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("word.txt");
                using StreamReader reader = new StreamReader(fileStream);
                while ((line = reader.ReadLine()) != null)
                {
                    rPage = new MainPage();
                    rPage.CorrectWord = line;
                }
            }
            catch (Exception ex)
            {
                lengthoflist = -1;
                MainPage error = new MainPage();
                error.CorrectWord = ex.ToString();
            }
        }
        public ListofWords()
        {
            words = new List<string>();
            string savedfilelocation = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "word.txt");
            if (File.Exists(savedfilelocation))
            {
                ReadFileIntoList();
            }
            else
            {
            }
        }
        public string GiveWord()
        {
            int which = random.Next(lengthoflist);
            return words[which];
        }
    }
}
