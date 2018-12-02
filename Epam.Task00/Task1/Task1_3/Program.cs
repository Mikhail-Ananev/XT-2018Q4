using System;
using System.Text;

namespace Square
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a positive integer odd number:");
            int x;
            bool result = Int32.TryParse(Console.ReadLine(), out x);

            if (result && x > 0 && x % 2 != 0)
            {
                ShowStars(x);
            }
            else
            {
                Console.WriteLine("Invalid value entered.");
            }
        }

        public static void ShowStars(int n)
        {
            for (int i = 0; i < n*n; i++)
            {
                if (i != 0 && i % n == 0)
                    {
                        Console.WriteLine(); ;
                    }
                if (i != n * n / 2)
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
