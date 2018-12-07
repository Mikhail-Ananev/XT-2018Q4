using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.Lost
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            Console.WriteLine("WARNING: if you input number more than 300 you will see bad result.");
            Console.WriteLine("Input quantity:");
            int k;
            bool result = int.TryParse(Console.ReadLine(), out k);
            if (!result)
            {
                throw new ArgumentException("Wrong input");
            }

            Lost arr = new Lost(k);
            int counter;
            for (int i = 0; i < (k / 2) + 1; i++)
            {
                counter = 0;
                foreach (var item in arr)
                {
                    counter++;
                    Console.Write($"{item} ");
                }

                Console.WriteLine();
                if (counter == 1)
                {
                    break;
                }
            }
        }
    }
}