using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.Lost
{
    public class GetLostEnumerator : IEnumerator<int>
    {
        private static long n;
        private int currentIndex = -1;
        private int[] sourceArr;

        public GetLostEnumerator(int[] arr)
        {
            this.sourceArr = arr;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }
        
        public int Current
        {
            get
            {
                return this.sourceArr[this.currentIndex * n];
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            ++this.currentIndex;
            if (this.currentIndex == 0)
            {
                if (n == 0)
                {
                    n++;
                }
                else if (n < long.MaxValue / 2)
                {
                    n = n << 1;
                }
            }

            return this.currentIndex * n != this.sourceArr.Length && this.currentIndex * n < this.sourceArr.Length;
        }

        public void Reset()
        {
            this.currentIndex = -1;
        }
    }
}
