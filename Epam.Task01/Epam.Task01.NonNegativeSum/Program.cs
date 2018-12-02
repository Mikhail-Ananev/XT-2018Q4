using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.NonNegativeSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[15];

            CreateArray(array);
            ShowArray(array);

            Console.WriteLine("Sum of positive numbers in array is: {0}", NonNegativeSum(array));
        }


        public static void CreateArray(int[] arr)
        {
            var rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(-999, 999);
            }
        }

        public static void ShowArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("{0,-5}", arr[i]);
            }
            Console.WriteLine();
        }

        public static int NonNegativeSum(params int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    sum += arr[i];
                }
            }
            return sum;
        }
    }
}
