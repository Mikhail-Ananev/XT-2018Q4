using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.DynamicArray
{
    public class DynamicArray<T>
    {
        private T[] dynArray;

        public DynamicArray()
        {
            this.dynArray = new T[8];
            this.Capacity = 8;
            this.DynLength = 0;
        }

        public DynamicArray(int capacity)
        {
            this.dynArray = new T[capacity];
            this.Capacity = capacity;
            this.DynLength = 0;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            this.dynArray = new T[collection.Count()];
            this.dynArray = collection.ToArray<T>();
        }

        public int Capacity { get; private set; }

        public int DynLength { get; private set; }

        public T this[int index]
        {
            get => this.dynArray[index];
            set
            {
                this.IndexValidation(index);
                this.dynArray[index] = value;
            }
        }

        public void Add(T add)
        {
            if (this.DynLength == this.Capacity)
            {
                this.Increase(2);
            }

            this.dynArray[this.DynLength] = add;
            this.DynLength++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            int vacancy = this.Capacity - this.DynLength;
            if (vacancy < collection.Count())
            {
                int n = ((collection.Count() - vacancy) / this.Capacity) + 1;
                this.Increase(n);
            }

            this.MakeAddRange(collection);
        }

        public bool Insert(T add, int index)
        {
            //IndexValidation(index);
            return false;
        }

        private void Increase(int n)
        {
            T[] newArr = new T[this.Capacity * n];
            this.Copy(newArr);
            this.dynArray = newArr;
            this.Capacity *= n;
        }

        private void Copy(T[] newArr)
        {
            for (int i = 0; i < this.DynLength; i++)
            {
                newArr[i] = this.dynArray[i];
            }
        }

        private void MakeAddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.dynArray[this.DynLength] = item;
                this.DynLength++;
            }
        }

        private void IndexValidation(int index)
        {
            if (index >= this.DynLength)
            {
                throw new ArgumentOutOfRangeException("Index more than array range!", nameof(index));
            }
        }
    }
}
