using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.ArrayProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[15];

            CreateArray(array);
            ShowArray(array);
            SortArray(array);
            ShowArray(array);

            Console.WriteLine($"Min value {array[0]}" + Environment.NewLine + $"Max value {array[array.Length - 1]}");
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
                Console.Write("{0,5}",arr[i]);
            }
            Console.WriteLine();
        }
        
        public static void SortArray(int[] arr)
        {
            int temp;
            //int counter;
            for (int i = 0; i < arr.Length; i++)
            {
                //counter = 0;
                for (int j = i; j < arr.Length-i-1; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        temp = arr[j];
                        arr[j] = arr[i];
                        arr[i] = temp;
                        //counter++;
                    }
                    if (arr[arr.Length - i - 1] < arr[j])
                    {
                        temp = arr[j];
                        arr[j] = arr[arr.Length - i - 1];
                        arr[arr.Length - i - 1] = temp;
                        //counter++;
                    }
                }
                //Console.WriteLine("{0}  {1}",counter,i);
                //if (counter < 1)
                //{
                //    break;
                //}
            }
        }
    }
}
