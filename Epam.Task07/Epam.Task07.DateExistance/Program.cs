using System;
using System.Text.RegularExpressions;

namespace Epam.Task07.DateExistance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input text with date in format dd-mm-yyyy:");
            string test = Console.ReadLine();

            Regex regex = new Regex(@"\b((31-(0[13578]|1[02])|30-(0[13-9]|1[012])|(0[1-9]|1\d|2[0-8])-(0[1-9]|1[012]))-\d{4})|(29-02-\d\d([02468][048])|([13579][26]))\b");

            if (regex.IsMatch(test))
            {
                Console.WriteLine("Text: \"" + test + "\" contains date.");
            }
            else
            {
                Console.WriteLine("Text: \"" + test + "\" don`t contains date.");
            }
        }
    }
}
