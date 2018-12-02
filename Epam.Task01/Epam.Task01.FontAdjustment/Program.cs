using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.FontAdjustment
{
    class Program
    {
        static void Main(string[] args)
        {
            Style style = 0;
            Console.WriteLine("String style:{0}", style);
            do
            {
                
                Console.WriteLine("Enter:" + Environment.NewLine +
                    "\t1: bold" + Environment.NewLine +
                    "\t2: italic" + Environment.NewLine +
                    "\t3: underline");
                
                
                if (Int32.TryParse(Console.ReadLine(), out int n))
                {
                    switch (n)
                    {
                        case 1:
                            style ^= Style.bold;
                            break;
                        case 2:
                            style ^= Style.italic;
                            break;
                        case 3:
                            style ^= Style.underline;
                            break;
                        default:
                            Console.WriteLine("Incorrect input.");
                            break;
                    }
                    Console.WriteLine("String style:{0}", style);
                    Console.WriteLine("To exit the cycle input 'exit', to continue press 'Enter'");

                }
                else
                {
                    Console.WriteLine("Incorrect input.");
                    Console.WriteLine("To exit the cycle input 'exit', to continue press 'Enter'");
                }
            } while (!string.Equals(Console.ReadLine(), "exit"));
            
        }
        [Flags]
        enum Style
        {
            None = 0,
            bold = 1,
            italic = 2,
            underline = 4,
        }
    }
}
