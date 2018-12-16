using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ISeekYou
{
    public class Program
    {
        public static long FindMedian(long[] arr)
        {
            Array.Sort(arr);
            return arr[arr.Length / 2];
        }

        public static IEnumerable<int> FindeElement(int[] arr)
        {
            foreach (int x in arr)
            {
                if (x >= 0)
                {
                    yield return x;
                }
            }
        }

        public static bool IsPositiv(int x)
        {
            return x >= 0;
        }

        public static IEnumerable<int> FindeElement(int[] arr, Func<int, bool> predicate)
        {
            foreach (int x in arr)
            {
                if (predicate(x))
                {
                    yield return x;
                }
            }
        }

        public static void Main(string[] args)
        {
            int[] array = new int[3000000];
            int c = 0;
            long[] time = new long[200];
            Random rnd = new Random();
            Stopwatch stopWatch = new Stopwatch();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(-999, 999);
            }

            Console.WriteLine("------------------------------------------------------");
            for (int i = 0; i < time.Length; i++)
            {
                stopWatch.Restart();
                c = FindeElement(array).Count();
                stopWatch.Stop();
                time[i] = stopWatch.ElapsedMilliseconds;
            }

            Console.WriteLine("Average time to simple call: " + FindMedian(time));
            Console.WriteLine("------------------------------------------------------");
            for (int i = 0; i < time.Length; i++)
            {
                stopWatch.Restart();
                c = FindeElement(array, IsPositiv).Count();
                stopWatch.Stop();
                time[i] = stopWatch.ElapsedMilliseconds;
            }

            Console.WriteLine("Average time to call with delegate: " + FindMedian(time));
            Console.WriteLine("------------------------------------------------------");
            for (int i = 0; i < time.Length; i++)
            {
                stopWatch.Restart();
                c = FindeElement(array, delegate(int x) { return x >= 0; }).Count();
                stopWatch.Stop();
                time[i] = stopWatch.ElapsedMilliseconds;
            }

            Console.WriteLine("Average time to call with anonimous method: " + FindMedian(time));
            Console.WriteLine("------------------------------------------------------");
            for (int i = 0; i < time.Length; i++)
            {
                stopWatch.Restart();
                c = FindeElement(array, x => x >= 0).Count();
                stopWatch.Stop();
                time[i] = stopWatch.ElapsedMilliseconds;
            }

            Console.WriteLine("Average time to call with lambda: " + FindMedian(time));
            Console.WriteLine("------------------------------------------------------");
            for (int i = 0; i < time.Length; i++)
            {
                stopWatch.Restart();
                c = (from x in array
                     where x >= 0
                     select x).Count();
                stopWatch.Stop();
                time[i] = stopWatch.ElapsedMilliseconds;
            }

            Console.WriteLine("Average time to call with LINQ: " + FindMedian(time));
        }
    }
}