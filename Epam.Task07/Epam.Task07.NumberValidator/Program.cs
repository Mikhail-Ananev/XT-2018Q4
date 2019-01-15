using System;
using System.Text.RegularExpressions;

namespace Epam.Task07.NumberValidator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input number:");
            string test = Console.ReadLine();

            Regex regex3 = new Regex(@"(^(\+-)?[0-9]+\.?[0-9]+$)|(^(\+|-)?[1-9]\.?[0-9]*e[\+-]?[0-9]+$)");

            if (regex3.Match(test).Groups[1].Success)
            {
                Console.WriteLine("It`s simple notation number");
            }
            else if (regex3.Match(test).Groups[2].Success)
            {
                Console.WriteLine("It`s science notation number");
            }
            else
            {
                Console.WriteLine("It dosn`t number");
            }
        }
    }
}