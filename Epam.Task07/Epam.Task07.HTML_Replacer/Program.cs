using System;
using System.Text.RegularExpressions;

namespace Epam.Task07.HTML_Replacer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Source HTML code:");
            string test = "<p>Текст, который пользователь должен ввести с клавиатуры поместите в" +
                          "элемент - контейнер \"kbd\".</ p >" +
                          "< p > Введите текст: < kbd > Это — текст, вводимый с клавиатуры </ kbd ></ p >" +
                          "< p >< samp > Это — текст, который помещен в контейнер \"samp\"</ samp ></ p > ";

            Console.WriteLine(test);
            Console.WriteLine();
            Console.WriteLine("Result HTML code:");
            Regex regex = new Regex(@"<[^>]+>");
            string result = regex.Replace(test, "_");
            Console.WriteLine(result);
        }

    }
}
