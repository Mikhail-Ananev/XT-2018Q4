using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epam.Task04.CustomSort
{
    public class SortInThread<T> : ArraySort<T>
    {
        public SortInThread(T[] testArray, Func<T, T, int> comparer)
            : base(testArray, comparer)
        {
        }

        public delegate void MyEventHandler();

        public event MyEventHandler EndSort;

        public void RunThread()
        {
            Thread th1 = new Thread(this.SortAndPrint);
            th1.Start();
            Thread.Sleep(100);
        }

        private void SortAndPrint()
        {
            this.Sort();
            foreach (var item in this.SortArray)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine();
            this.EndSort();
        }
    }
}
