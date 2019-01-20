using System;
using System.Text.RegularExpressions;

namespace Epam.Task07.TimeCounter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input text with time:");
            string test = Console.ReadLine();

            Regex regex = new Regex(@"\b([0-1][0-9]|2[0-3]):[0-5][0-9]\b");

            Console.WriteLine("Time contains in this text {0} times", regex.Matches(test).Count);
        }
    }
}