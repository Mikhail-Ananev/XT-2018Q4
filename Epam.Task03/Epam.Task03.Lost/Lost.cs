using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.Lost
{
    class Lost : IEnumerable<int>
    {
        private int[] arrData;
        public Lost()
        {
            arrData = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        public Lost(int k)
        {
            if (k < 1)
            {
                throw new ArgumentException("Incorrect input", nameof(k));
            }
            arrData = new int[k];
            for (int i = 0; i < k; i++)
            {
                arrData[i] = i;
            }
        }


        public IEnumerator<int> GetEnumerator()
        {
            return new GetLostEnumerator(arrData);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
