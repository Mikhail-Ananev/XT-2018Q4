using System;
using System.Text.RegularExpressions;

namespace Epam.Task07.EmailFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input text with e-mail adress:");

            string test = "Pete: Pete@m.ru, Nemo: N@mail.ru, Nikolay: Ni.ivanov@mail.ru, Ivan: iv-an@mail.ru, Vasiliy: v_ivanov@mail.rol.ru";

            string pattern = @"\b[A-Za-z0-9]([A-Za-z0-9.\-_]*[A-Za-z0-9])*@([A-Za-z0-9]([A-Za-z0-9\-]*[A-Za-z0-9])*\.)+[A-Za-z]{2,6}\b";
            MatchCollection matches = Regex.Matches(test, pattern);
            Console.WriteLine("E-mail list:");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value, match.Index);
            }
        }
    }
}