using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATUWordle
{
    public class Player
    {
        public string name { get; set; }
        public int hm { get; set; }
        public int p1 { get; set; }
        public int p2 { get; set; }
        public int guesses { get; set; } = 1;
        public int turns { get; set; }

        public Player(string name, int howmany)
        {
            this.name = name;
            this.hm = howmany;
        }
        public async Task Turn()
        {
            turns = 1;
            if (turns == 1)
            {
                await Shell.Current.DisplayAlert(name, "Your turn " + name, "OK");
                turns = 2;
                return;
            }
            else
            {
                await Shell.Current.DisplayAlert(name, "Your turn " + name, "OK");
                turns = 1;
                return;
            }
        }

        public void Guess()
        {
            for (int i = 0; i < 6; i++)
            {
                guesses++;
            }
            return;
        }

       

        public async Task Winner()
        {
            Guess();
            p1 = guesses;
            p2 = guesses;
            if (p1 < p2)
                await Shell.Current.DisplayAlert("Winner", name, "OK");
            else
                await Shell.Current.DisplayAlert("Winner", name, "OK");
        }

    }
}
