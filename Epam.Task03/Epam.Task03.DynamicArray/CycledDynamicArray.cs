using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.DynamicArray
{
    public class CycledDynamicArray<T> : DynamicArray<T>
    {
        public CycledDynamicArray() : base()
        {
        }

        public CycledDynamicArray(int capacity) : base(capacity)
        {
        }

        public CycledDynamicArray(IEnumerable<T> collection) : base(collection)
        {
        }

        public override IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                for (int i = 0; i < this.Length; i++)
                {
                    yield return this.DynArray[i];
                }
            }
        }
    }
}
