using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a positive integer (> 1) :");
            int x;
            bool result = Int32.TryParse(Console.ReadLine(), out x);

            if (result && x > 1)
            {
                if (IsSimple(x))
                    Console.WriteLine("{0} is a simple number.", x);
                else
                    Console.WriteLine("{0} is not a simple number.", x);
            }
            else
                Console.WriteLine("Invalid value entered.");

        }

        public static bool IsSimple(int n)
        {
            int count = 0;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    count++;
            }
            if (count > 1)
                return false;
            else
                return true;
        }
    }
}
