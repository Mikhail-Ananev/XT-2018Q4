using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Round
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            Console.WriteLine("Input radius:");
            double n;
            bool result = double.TryParse(Console.ReadLine(), out n);
            Test(n, result);
            Round round = new Round
            {
                Radius = n
            };
            Console.WriteLine("If you want enter the coordinates of the center - press 'y':");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Input X:");
                result = double.TryParse(Console.ReadLine(), out n);
                Test(n, result);
                round.Center.X = n;
                Console.WriteLine("Input Y:");
                result = double.TryParse(Console.ReadLine(), out n);
                Test(n, result);
                round.Center.Y = n;
            }

            Console.WriteLine($"Area: {round.GetArea,2:f}{Environment.NewLine}" +
                $"Length: {round.GetLength,2:f}{Environment.NewLine}" +
                $"Radius: {round.Radius,2:f}{Environment.NewLine}" +
                $"X: {round.Center.X}{Environment.NewLine}" +
                $"Y: {round.Center.Y}");
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
