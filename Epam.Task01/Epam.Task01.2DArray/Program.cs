using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01._2DArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[7,7];

            CreateArray(array);
            ShowArray(array);

            Console.WriteLine("Sum of positive numbers in array is: {0}", Sum2D(array));
        }


        public static void CreateArray(int[,] arr)
        {
            var rnd = new Random();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    arr[i, j] = rnd.Next(-999, 999);
                }
                
            }
        }

        public static void ShowArray(int[,] arr)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write("{0,-5}", arr[i, j]);
                }
                Console.WriteLine();
            }
            
        }

        public static int Sum2D (int[,] arr)
        {
            int sum = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i + j) %2 == 0)
                    {
                        sum += arr[i,j];
                    }
                }
                
            }
            return sum;
        }
    }
}
