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
            var testDynamic = new DynamicArray<string>("Hello there! It`s the first string!".Split(' '));
            Show(testDynamic);
            testDynamic.Add("This is new element.");
            Show(testDynamic);
            testDynamic.RemoveByIndex(6);
            Show(testDynamic);
            testDynamic.AddRange("This is new elements after Remove".Split(' '));
            Show(testDynamic);
            testDynamic.Remove("Hello");
            Show(testDynamic);
            testDynamic.Insert("!! It was inserted !!", 3);
            Show(testDynamic);
            Console.WriteLine();
            string[] test = testDynamic.ToArray();
            Console.WriteLine("After 'ToArray':");
            foreach (var item in test)
            {
                Console.Write(item + " ");
            }

            var testCycled = new CycledDynamicArray<string>("Hello there! You'll see it forever...".Split(' '));
            int counter = 0;
            foreach (var item in testCycled)
            {
                Console.WriteLine(item);
                counter++;
                if (counter == 15)
                {
                    counter = 0;
                    Console.WriteLine("Press 'Enter' to continue...");
                    Console.ReadLine();
                }
            }
        }

        private static void Show(DynamicArray<string> collection)
        {
            foreach (var item in collection)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }
    }
}
