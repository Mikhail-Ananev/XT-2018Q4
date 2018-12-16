using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epam.Task04.CustomSort
{
    public class ArraySort<T>
    {
        private T[] sortArray;
        private Func<T, T, int> myComparer;

        public ArraySort(T[] testArray, Func<T, T, int> comparer)
        {
            this.SortArray = testArray;
            this.Comparer = comparer;
        }

        public T[] SortArray
        {
            protected get
            {
                return this.sortArray;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("передан null массив");
                }

                this.sortArray = value;
            }
        }

        public Func<T, T, int> Comparer
        {
            private get
            {
                return this.myComparer;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("передан null делегат сортировщика");
                }

                this.myComparer = value;
            }
        }

        public void Sort()
        {
            for (int i = 0; i < this.SortArray.Length; i++)
            {
                for (int j = i + 1; j < this.SortArray.Length; j++)
                {
                    if (this.Comparer(this.SortArray[i], this.SortArray[j]) > 0)
                    {
                        this.Swap(this.SortArray, i, j);
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
    }
}