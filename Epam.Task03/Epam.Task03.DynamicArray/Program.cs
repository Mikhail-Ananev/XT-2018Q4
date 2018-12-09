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
            var hello = new DynamicArray<string>("Hello there! It`s the first string!".Split(' '));
            Show(hello);
            hello.Add("This is new element.");
            Show(hello);
            hello.RemoveByIndex(6);
            Show(hello);
            hello.AddRange("This is new elements after Remove".Split(' '));
            Show(hello);
            hello.Remove("Hello");
            Show(hello);
            hello.Insert("!! It was inserted !!", 3);
            Show(hello);
            Console.WriteLine();
            string[] test = hello.ToArray();
            Console.WriteLine("After 'ToArray':");
            foreach (var item in test)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            var dynamic = new DynamicArray<int>();
            Console.WriteLine(dynamic.Capacity);
            dynamic.Add(76);
            Console.WriteLine(dynamic.Capacity);
            dynamic.Add(76);
            Console.WriteLine(dynamic.Capacity);
            var stringArray = new DynamicArray<string>("This is first".Split(' '));
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
