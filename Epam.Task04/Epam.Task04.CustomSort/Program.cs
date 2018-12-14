using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task04.CustomSort
{
    public class Program
    {
        public static int CompareByLength(string str1, string str2)
        {
            if (str1 == null || str2 == null)
            {
                throw new ArgumentException("One or both parameters 'null'!");
            }

            if (str1.Length == str2.Length)
            {
                return 0;
            }

            if (str1.Length > str2.Length)
            {
                return 1;
            }

            return -1;
        }

        public static int CompareByChar(string str1, string str2)
        {
            int minLength = Program.CompareByLength(str1, str2) < 0 ? str1.Length : str2.Length;
            for (int i = 0; i < minLength; i++)
            {
                if (str1[i] != str2[i])
                {
                    if (str1[i] > str2[i])
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        protected static void Main(string[] args)
        {
            string[] testArray = new[]
            {
                "Пицик",
                "Ананьев",
                "Баранов",
                "Ануфриев",
                "Иванов",
                "Дворников",
                "Федоров",
                "Пушкин",
            };

            var test1 = new ArraySort<string>();
            test1.Sort(testArray, CompareByChar);
            for (int t = 0; t < testArray.Length; t++)
            {
                Console.WriteLine(testArray[t]);
            }
        }
    }
}