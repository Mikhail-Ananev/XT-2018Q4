using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a positive integer:");
            int x;
            bool result = Int32.TryParse(Console.ReadLine(), out x);

            if (result && x > 0)
            {
                Show(x);
            }
            else
            {
                Console.WriteLine("Invalid value entered.");
            }
        }

        public static void Show(int n)
        {

            for (int i = 1; i <= n; i++)
            {
                Console.Write(i);
                if (i < n)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }
    }
}
