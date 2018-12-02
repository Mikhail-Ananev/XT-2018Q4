using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.X_masTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input positive integer:");

            bool result = Int32.TryParse(Console.ReadLine(), out int n);

            if (result && n > 0)
            {
                DrawXmasTree(n);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        public static void DrawXmasTree(int n)
        {
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < k + 1; i++)
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
}
