using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.DynamicArray
{
    public class DynamicArray<T> : IEnumerable, IEnumerable<T>, ICloneable
    {
        private int capacity;

        public DynamicArray()
        {
            this.DynArray = new T[8];
            this.Capacity = 8;
            this.Length = 0;
        }

        public DynamicArray(int capacity)
        {
            this.DynArray = new T[capacity];
            this.Capacity = capacity;
            this.Length = 0;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            this.DynArray = new T[collection.Count()];
            this.DynArray = collection.ToArray<T>();
            this.Capacity = collection.Count();
            this.Length = collection.Count();
        }

        public T[] DynArray { get; private set; }

        public int Capacity
        {
            get => this.capacity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Capacity must be positive!");
                }

                this.capacity = value;
                if (value == 0)
                {
                    this.DynArray = null;
                    this.capacity = value;
                    this.Length = value;
                }

                if (value > this.capacity)
                {
                    T[] newArray = new T[value];
                    this.Copy(newArray);
                    this.DynArray = newArray;
                    this.capacity = value;
                }

                if (value < this.capacity)
                {
                    if (value < this.Length)
                    {
                        this.Length = value;
                    }

                    T[] newArray = new T[value];
                    this.Copy(newArray);
                    this.DynArray = newArray;
                    this.capacity = value;
                }
            }
        }

        public int Length { get; private set; }

        public T this[int index]
        {
            get
            {
                if (!this.IndexValidation(index))
                {
                    throw new ArgumentOutOfRangeException("Index more than array range!", nameof(index));
                }

                if (index < 0)
                {
                    return this.DynArray[this.Length + index];
                }

                return this.DynArray[index];
            }

            set
            {
                if (!this.IndexValidation(index))
                {
                    throw new ArgumentOutOfRangeException("Index more than array range!", nameof(index));
                }

                this.DynArray[index] = value;
            }
        }

        public void Add(T add)
        {
            this.CheckAndIncreaseCapacityToAdd();
            this.DynArray[this.Length] = add;
            this.Length++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            int vacancy = this.Capacity - this.Length;
            if (vacancy < collection.Count())
            {
                int n = ((collection.Count() - vacancy) / this.Capacity) + 1;
                this.Increase(n);
            }

            this.MakeAddRange(collection);
        }

        public bool Insert(T add, int index)
        {
            if (!this.IndexValidation(index))
            {
                return false;
            }

            this.CheckAndIncreaseCapacityToAdd();
            for (int i = this.Length; i > index; i--)
            {
                this.DynArray[i] = this.DynArray[i - 1];
            }

            this.DynArray[index] = add;
            this.Length++;
            return true;
        }

        public bool Remove(T item)
        {
            int index = this.FindIndex(item);
            return this.RemoveByIndex(index);
        }

        public bool RemoveByIndex(int index)
        {
            if (index < 0)
            {
                return false;
            }

            for (int i = index; i < this.Length - 1; i++)
            {
                this.DynArray[i] = this.DynArray[i + 1];
            }

            this.Length--;
            this.DynArray[this.Length] = default(T);
            return true;
        }

        public int FindIndex(T item)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if (this.DynArray[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public T[] ToArray()
        {
            T[] toArray = new T[this.Length];
            this.Copy(toArray);
            return toArray;
        }

        public object Clone()
        {
            return new DynamicArray<T>(this);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this.DynArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void CheckAndIncreaseCapacityToAdd()
        {
            if (this.Length == this.Capacity)
            {
                this.Increase(2);
            }
        }

        private void Increase(int n)
        {
            T[] newArr = new T[this.Capacity * n];
            this.Copy(newArr);
            this.DynArray = newArr;
            this.Capacity *= n;
        }

        private void Copy(T[] newArr)
        {
            for (int i = 0; i < this.Length; i++)
            {
                newArr[i] = this.DynArray[i];
            }
        }

        private void MakeAddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.DynArray[this.Length] = item;
                this.Length++;
            }
        }

        private bool IndexValidation(int index)
        {
            if (index > 0)
            {
                if (index >= this.Length)
                {
                    return false;
                }

                return true;
            }

            if (this.Length + index < 0)
            {
                return false;
            }

            return true;
        }
    }
}