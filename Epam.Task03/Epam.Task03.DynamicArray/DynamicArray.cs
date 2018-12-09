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
        internal T[] dynArray;
        private int capacity;

        public DynamicArray()
        {
            this.dynArray = new T[8];
            this.Capacity = 8;
            this.Length = 0;
        }

        public DynamicArray(int capacity)
        {
            this.dynArray = new T[capacity];
            this.Capacity = capacity;
            this.Length = 0;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            this.dynArray = new T[collection.Count()];
            this.dynArray = collection.ToArray<T>();
            this.Capacity = collection.Count();
            this.Length = collection.Count();
        }

        public int Capacity
        {
            get => this.capacity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Capacity must be positive!");
                }

                capacity = value;
                if (value == 0)
                {
                    this.dynArray = null;
                    this.capacity = value;
                    this.Length = value;
                }

                if (value > this.capacity)
                {
                    T[] newArray = new T[value];
                    Copy(newArray);
                    this.dynArray = newArray;
                    this.capacity = value;
                }

                if (value < this.capacity)
                {
                    if (value < this.Length)
                    {
                        this.Length = value;
                    }

                    T[] newArray = new T[value];
                    Copy(newArray);
                    this.dynArray = newArray;
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
                    return this.dynArray[this.Length + index];
                }

                return this.dynArray[index];
            } 
            set
            {
                if (!this.IndexValidation(index))
                {
                    throw new ArgumentOutOfRangeException("Index more than array range!", nameof(index));
                }

                this.dynArray[index] = value;
            }
        }

        public void Add(T add)
        {
            CheckAndIncreaseCapacityToAdd();
            this.dynArray[this.Length] = add;
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

            CheckAndIncreaseCapacityToAdd();
            for (int i = this.Length; i > index; i--)
            {
                this.dynArray[i] = this.dynArray[i - 1];
            }

            this.dynArray[index] = add;
            this.Length++;
            return true;
        }

        public bool Remove(T item)
        {
            int index = FindIndex(item);
            //if (index < 0)
            //{
            //    return false;
            //}

            //for (int i = index; i < this.Length - 1; i++)
            //{
            //    this.dynArray[i] = this.dynArray[i + 1];
            //}

            //this.Length--;
            //this.dynArray[this.Length] = default(T);
            return RemoveByIndex(index);
        }

        public bool RemoveByIndex(int index)
        {
            if (index < 0)
            {
                return false;
            }

            for (int i = index; i < this.Length - 1; i++)
            {
                this.dynArray[i] = this.dynArray[i + 1];
            }

            this.Length--;
            this.dynArray[this.Length] = default(T);
            return true;
        }


        public int FindIndex(T item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this.dynArray[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public T[] ToArray()
        {
            T[] toArray = new T[this.Length];
            Copy(toArray);
            return toArray;
        }

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
            this.dynArray = newArr;
            this.Capacity *= n;
        }

        private void Copy(T[] newArr)
        {
            for (int i = 0; i < this.Length; i++)
            {
                newArr[i] = this.dynArray[i];
            }
        }

        private void MakeAddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.dynArray[this.Length] = item;
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

            if (this.Length + index < 0 )
            {
                return false;
            }

            return true;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this.dynArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public object Clone()
        {
            return new DynamicArray<T>(this);
        }
    }
}