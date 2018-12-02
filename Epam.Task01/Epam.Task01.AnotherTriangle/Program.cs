using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.AnotherTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input positive integer:");

            bool result = Int32.TryParse(Console.ReadLine(), out int n);

            if (result && n > 0)
            {
                DrawAnotherTriangle(n);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        public static void DrawAnotherTriangle(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + i; j++)
                {
                    if (j + i >= n - 1)
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
