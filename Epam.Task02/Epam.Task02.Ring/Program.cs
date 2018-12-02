using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Round
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input outer radius:");
            double outer;
            bool result = double.TryParse(Console.ReadLine(), out outer);
            Test(outer, result);
            Console.WriteLine("Input inner radius:");
            double inner;
            result = double.TryParse(Console.ReadLine(), out inner);
            Test(inner, result);

            Ring ring = new Ring(outer, inner);
            Console.WriteLine("If you want enter the coordinates of the center - press 'y':");
            if (Console.ReadLine() == "y")
            {
                double n;
                Console.WriteLine("Input X:");
                result = double.TryParse(Console.ReadLine(), out n);
                Test(n, result);
                ring.Center.X = n;
                Console.WriteLine("Input Y:");
                result = double.TryParse(Console.ReadLine(), out n);
                Test(n, result);
                ring.Center.Y = n;
            }

            Console.WriteLine($"Ring area: {ring.RingArea,2:f}{Environment.NewLine}" +
                $"Ring length: {ring.RingLength,2:f}{Environment.NewLine}" +
                $"Outer radius: {ring.OuterRadius,2:f}{Environment.NewLine}" +
                $"Inner radius: {ring.InnerRadius,2:f}{Environment.NewLine}" +
                $"X: {ring.Center.X}{Environment.NewLine}" +
                $"Y: {ring.Center.Y}");
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
