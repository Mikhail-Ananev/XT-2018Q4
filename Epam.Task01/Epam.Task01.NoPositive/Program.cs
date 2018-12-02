using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.NoPositive
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,,] array = new int[5,5,2];

            CreateArray(array);
            Console.WriteLine("Original array:");
            ShowArray(array);
            NoPositiveArray(array);
            Console.WriteLine("Array after modification:");
            ShowArray(array);
        }

        private static void NoPositiveArray(int[,,] arr)
        {
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (arr[j, i, k] > 0)
                        {
                            arr[j, i, k] = 0;
                        }
                    }
                }
            }
        }

        private static void ShowArray(int[,,] arr)
        {
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Console.Write("{0,5}", arr[j,i,k]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void CreateArray(int[,,] arr)
        {
            var rnd = new Random();
            //foreach (int item in arr)
            //{

            //}
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        arr[j,i,k] = rnd.Next(-999, 999);
                    }
                }
            }
        }
    }
}
