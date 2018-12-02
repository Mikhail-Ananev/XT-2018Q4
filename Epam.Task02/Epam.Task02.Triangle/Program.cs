using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            Console.WriteLine("Input triangle side 'a':");
            double sideA;
            bool result = double.TryParse(Console.ReadLine(), out sideA);
            Test(sideA, result);

            Console.WriteLine("Input triangle side 'b':");
            double sideB;
            result = double.TryParse(Console.ReadLine(), out sideB);
            Test(sideB, result);

            Console.WriteLine("Input triangle side 'c':");
            double sideC;
            result = double.TryParse(Console.ReadLine(), out sideC);
            Test(sideC, result);

            Triangle triangle = new Triangle
            {
                A = sideA,
                B = sideB,
                C = sideC
            };
            Console.WriteLine($"Area: {triangle.GetTriangleArea,2:f}{Environment.NewLine}" +
                $"Perimeter: {triangle.GetTrianglePerimeter,2:f}{Environment.NewLine}" +
                $"Side 'a': {triangle.A}{Environment.NewLine}" +
                $"Side 'b': {triangle.B}{Environment.NewLine}" +
                $"Side 'c': {triangle.C}");
        }

        private static void Test(double n, bool result)
        {
            if (!result)
            {
                throw new ArgumentException("Wrong input value:", nameof(n));
            }
        }
    }
}