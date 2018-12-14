using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToIntOrNotToInt
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            var metod = new MyDigitMethod();
            Console.WriteLine("123 is positive integer?  " + metod.IsDigit("123"));
            Console.WriteLine();
            Console.WriteLine("0012340.000E-1 is positive integer?  " + metod.IsDigit("0012340.000E-1"));
            Console.WriteLine();
            Console.WriteLine("-0012340000.000 is positive integer?  " + metod.IsDigit("-0012340000.000"));
            Console.WriteLine();
            Console.WriteLine("0012340.000E-1 is positive integer?  " + metod.IsDigit("0012340.000E-1"));
            Console.WriteLine();
            Console.WriteLine("12340000.000e-4 is positive integer?  " + metod.IsDigit("12340000.000e-4"));
            Console.WriteLine();
            Console.WriteLine("12340000e-5 is positive integer?  " + metod.IsDigit("12340000e-5"));
            Console.WriteLine();
            Console.WriteLine("340000,000 is positive integer?  " + metod.IsDigit("340000,000"));
            Console.WriteLine();
            Console.WriteLine("123.00006e4 is positive integer?  " + metod.IsDigit("123.00006e4"));
            Console.WriteLine();
            Console.WriteLine("123.00006e4 is positive integer?  " + metod.IsDigit("123.00006e4"));
            Console.WriteLine();
            Console.WriteLine("123400.0e0 is positive integer?  " + metod.IsDigit("123400.0e0"));
            Console.WriteLine();
            Console.WriteLine("00.0e98 is positive integer?  " + metod.IsDigit("00.0e98"));
            Console.WriteLine();
            Console.WriteLine("00.00000001e0 is positive integer?  " + metod.IsDigit("00.0e98"));
            Console.WriteLine();
            Console.WriteLine(".09 is positive integer?  " + metod.IsDigit(".09"));
            Console.WriteLine();
            Console.WriteLine(".009e3 is positive integer?  " + metod.IsDigit(".009e3"));
            Console.WriteLine();
        }
    }
}