﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task04.CustomSort
{
    public class Program
    {
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
            var test1 = new ArraySort<string>(testArray, SortMethod.CompareByLength);
            test1.Sort();
            for (int t = 0; t < testArray.Length; t++)
            {
                Console.WriteLine(testArray[t]);
            }

            Console.WriteLine();
            Console.WriteLine("New sorting method:");
            var test2 = new ArraySort<string>(testArray, SortMethod.CompareByChar);
            test2.Sort();
            for (int t = 0; t < testArray.Length; t++)
            {
                Console.WriteLine(testArray[t]);
            }
        }
    }
}