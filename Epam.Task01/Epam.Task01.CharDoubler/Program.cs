using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.CharDoubler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input first string:");
            string str1 = Console.ReadLine();
            Console.WriteLine("Input second string:");
            string str2 = (Console.ReadLine()).ToLower();

            Console.WriteLine($"Your result: {Environment.NewLine}{GetDoubleCharString(str1, str2)}");

        }

        private static string GetDoubleCharString(string str1, string str2)
        {
            if (str1 == null || str2 == null)
            {
                return str1;
            }
            var sb = new StringBuilder(str1.Length * 2);
            for (int i = 0; i < str1.Length; i++)
            {
                sb.Append(str1[i]);

                if (str2.IndexOf(char.ToLower(sb[sb.Length - 1])) >= 0)
                {
                    sb.Append(str1[i]);
                }
            }
            return sb.ToString();
        }
    }
}
