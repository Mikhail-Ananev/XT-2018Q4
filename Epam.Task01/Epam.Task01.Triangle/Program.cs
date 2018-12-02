using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input positive integer:");

            bool result = Int32.TryParse(Console.ReadLine(), out int n);

            if (result && n > 0)
            {
                DrawTriangle(n);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        public static void DrawTriangle(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }
    }
}
