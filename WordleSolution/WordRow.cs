using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATUWordle
{
    public class WordRow
    {
        public WordRow()
        {
            Letters = new Letter[5]
            {
            new Letter(),
            new Letter(),
            new Letter(),
            new Letter(),
            new Letter()
            };
        }

        public Letter[] Letters { get; set; }
        public bool Validate(char[] correctAnswer)
        {


            int count = 0;


            for (int i = 0; i < Letters.Length; i++)
            {
                var letter = Letters[i];
                if (letter.Input == correctAnswer[i])
                {
                    letter.Color = Colors.Yellow;
                    count++;
                }
                else if (correctAnswer.Contains(letter.Input))
                {
                    letter.Color = Colors.Green;
                }
                else
                {
                    letter.Color = Colors.Orange;
                }
            }

            return count == 5;
        }
    }
    public partial class Letter
    {
        public char Input { get; set; }
        public Color Color { get; set; }
    }
}

