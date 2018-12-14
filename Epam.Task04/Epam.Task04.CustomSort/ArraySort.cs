using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task04.CustomSort
{
    public class ArraySort<T>
    {
        public delegate int MyCompare(T first, T second);

        public void Sort(T[] myArray, MyCompare myCompare)
        {
            for (int i = 0; i < myArray.Length; i++)
            {
                for (int j = i + 1; j < myArray.Length; j++)
                {
                    if (myCompare(myArray[i], myArray[j]) > 0)
                    {
                        this.Swap(myArray, i, j);
                    }
                }
            }
        }

        private void Swap(T[] stringArray, int i, int j)
        {
            T temp = stringArray[i];
            stringArray[i] = stringArray[j];
            stringArray[j] = temp;
        }
        
        //public void QuickSort(int[] array, int left, int right)
        //{
        //    int i = left, j = right;
        //    int p = array[(left + right) / 2];
        //    int temp;
        //    while (i <= j)
        //    {
        //        while (array[i].CompareTo(p) < 0)
        //        {
        //            i++;
        //        }
        //        while (array[j].CompareTo(p) > 0)
        //        {
        //            j--;
        //        }

        //        if (i <= j)
        //        {
        //            temp = array[i];
        //            array[i] = array[j];
        //            array[j] = temp;
        //        }

        //        if (i < right)
        //        {
        //            QuickSort(array, i, right);
        //        }

        //        if (left < j)
        //        {
        //            QuickSort(array, left, j);
        //        }
        //    }
        //}


        //private void QuickStringArraySort<T>(T[] stringArray, int left, int right)
        //{
        //    int i = left;
        //    int j = right;
        //    int p = stringArray[(i + j) / 2].Length;
        //    while (i <= j)
        //    {
        //        while (stringArray[i].Length.CompareTo(p) < 0)
        //        {
        //            i++;
        //        }

        //        while (stringArray[j].Length.CompareTo(p) > 0)
        //        {
        //            j--;
        //        }

        //        if (i <= j)
        //        {
        //            Swap(stringArray, i, j);
        //            i++;
        //            j--;
        //        }

        //        if (i < right)
        //        {
        //            QuickStringArraySort(stringArray, i, right);
        //        }

        //        if (left < j)
        //        {
        //            QuickStringArraySort(stringArray, left, j);
        //        }
        //    }
        //}
    }
}
