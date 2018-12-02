using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.SumOfNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum3 = 334 * 999 / 2; //     n = 999/3 + 1      (a1 = 0, aN = 999)
            int sum5 = 200 * 995 / 2; //     n = 995/5 + 1      (a1 = 0, aN = 995)
            int sum15 = 67 * 990 / 2; //     n = 990/15 + 1     (a1 = 0, aN = 990)
            Console.WriteLine(sum3+sum5-sum15);
        }
    }
}
