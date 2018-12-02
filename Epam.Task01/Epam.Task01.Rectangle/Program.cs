using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Rectangle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input positive integer 'a':");

            bool result1 = Int32.TryParse(Console.ReadLine(), out int a);

            Console.WriteLine("Input positive integer 'b':");
            bool result2 = Int32.TryParse(Console.ReadLine(), out int b);

            if (result1 && result2)
            {
                if (a > 0 && b > 0)
                {
                    Console.WriteLine("The area of a rectangle with sides 'a' = {0} and 'b' = {1} is: {2}", a, b, RectangleArea(a,b));
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
           
        }
        public static long RectangleArea(int k, int l)
        {
            return k * l;
        }
    }
}
