using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.DynamicArray
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            var dynamic = new DynamicArray<int>(1);
            Console.WriteLine(dynamic.Capacity);
            dynamic.Add(76);
            Console.WriteLine(dynamic.Capacity);
            dynamic.Add(76);
            Console.WriteLine(dynamic.Capacity);
        }
    }
}
