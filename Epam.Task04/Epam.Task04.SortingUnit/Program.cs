using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task04.CustomSort
{
    public class Program
    {
        public static void SortDone()
        {
            Console.WriteLine("Sorting finished!");
            Console.WriteLine();
        }

        protected static void Main(string[] args)
        {
            string[] testArray = new[]
            {
                "Пицик",
                "Ананьев",
                "Баранов",
                "Ануфриев",
                "Иванов",
                "Дворников",
                "Федоров",
                "Пушкин",
            };
            var test1 = new SortInThread<string>(testArray, SortMethod.CompareByLength);
            test1.EndSort += SortDone;
            test1.RunThread();
            Console.WriteLine("New sorting method:");
            var test2 = new SortInThread<string>(testArray, SortMethod.CompareByChar);
            test2.EndSort += SortDone;
            test2.RunThread();
        }
    }
}