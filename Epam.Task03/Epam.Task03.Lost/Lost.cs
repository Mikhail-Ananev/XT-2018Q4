using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.Lost
{
    public class Lost : IEnumerable<int>
    {
        private int[] arrData;

        public Lost()
        {
            this.arrData = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        public Lost(int k)
        {
            if (k < 1)
            {
                throw new ArgumentException("Incorrect input", nameof(k));
            }

            this.arrData = new int[k];
            for (int i = 0; i < k; i++)
            {
                this.arrData[i] = i;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new GetLostEnumerator(this.arrData);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
