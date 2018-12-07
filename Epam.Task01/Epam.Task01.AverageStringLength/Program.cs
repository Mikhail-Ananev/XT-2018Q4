using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.AverageStringLength
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input string:");
            string str = Console.ReadLine();
            Console.WriteLine("Average string lenght: {0,6}", AverageStringLength(str));
        }

        public static double AverageStringLength(string s)
        {
            int sumSymbols = 0;
            int sumWords = 0;
            switch (s.Length)
            {
                case 0:
                    return 0;
                case 1:
                    if (char.IsLetter(s[0]))
                    {
                        return 1;
                    }
                    else return 0;
                default:
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (i < s.Length - 1)
                        {
                            if (i == 0 && char.IsLetter(s[i]))
                            {
                                sumWords++;
                            }

                            if (!char.IsLetter(s[i]) && char.IsLetter(s[i + 1]))
                            {
                                sumWords++;
                            }

                            if (char.IsLetter(s[i]))
                            {
                                sumSymbols++;
                            }
                        }

                        if (i == s.Length - 1 && char.IsLetter(s[i]))
                        {
                            sumSymbols++;
                        }
                    }

                    return sumSymbols / sumWords;
            }
        }
    }
}
