using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyString
{
    public class MyString
    {
        private char[] charArray;

        public MyString()
        {
            this.MyLength = 0;
            this.Capacity = 16;
        }

        public MyString(char[] charArray)
        {
            this.MyLength = charArray.Length;
            this.Capacity = charArray.Length;
            this.charArray = charArray;
        }

        public int Capacity { get; set; }

        public int MyLength { get; set; }

        public char this[int index]
        {
            get => this.charArray[index];
            set
            {
                this.charArray[index] = value;
            }
        }

        public static void Show(MyString charArray)
        {
            for (int i = 0; i < charArray.MyLength; i++)
            {
                Console.Write(charArray[i]);
            }

            Console.WriteLine();
        }

        public static void Show(char[] charArray)
        {
            for (int i = 0; i < charArray.Length; i++)
            {
                Console.Write(charArray[i]);
            }

            Console.WriteLine();
        }

        public static char[] Concat(char[] charArray1, char[] charArray2)
        {
            long finalLength = charArray1.Length + charArray2.Length;
            char[] newCharArray = new char[finalLength];
            for (int i = 0; i < finalLength; i++)
            {
                if (i >= charArray1.Length)
                {
                    newCharArray[i] = charArray2[i - charArray1.Length];
                }
                else
                {
                    newCharArray[i] = charArray1[i];
                }
            }

            return newCharArray;
        }

        public static char[] Concat(char[] charArray1, char[] charArray2, char[] charArray3)
        {
            long finalLength = charArray1.Length + charArray2.Length + charArray3.Length;
            char[] newCharArray = new char[finalLength];
            for (int i = 0; i < finalLength; i++)
            {
                if (i >= charArray1.Length)
                {
                    if (i >= charArray1.Length + charArray2.Length)
                    {
                        newCharArray[i] = charArray3[i - charArray1.Length - charArray2.Length];
                    }
                    else
                    {
                        newCharArray[i] = charArray2[i - charArray1.Length];
                    }
                }
                else
                {
                    newCharArray[i] = charArray1[i];
                }
            }

            return newCharArray;
        }

        public static char[] MyStringToCharArray(MyString myString)
        {
            char[] convert = new char[myString.MyLength];
            for (int i = 0; i < convert.Length; i++)
            {
                convert[i] = myString[i];
            }

            return convert;
        }

        public static MyString CharArrayToMyString(char[] charString)
        {
            MyString myString = new MyString(charString);
            return myString;
        }

        public static bool MyEqual(char[] string1, char[] string2)
        {
            if (string1 == null && string2 == null)
            {
                return true;
            }

            if (string1 == null || string2 == null)
            {
                return false;
            }

            if (string1.Length != string2.Length)
            {
                return false;
            }

            for (int i = 0; i < string1.Length; i++)
            {
                if (string1[i] != string2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static int FirstIndex(char[] array, char symbol)
        {
            if (array == null)
            {
                return -1;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == symbol)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int LastIndex(char[] array, char symbol)
        {
            if (array == null)
            {
                return -1;
            }

            for (int i = array.Length - 1; i > -1; i--)
            {
                if (array[i] == symbol)
                {
                    return i;
                }
            }

            return -1;
        }

        public char[] Append(params char[] add)
        {
            if (add != null)
            {
                while (this.MyLength + add.Length > this.Capacity)
                {
                    this.charArray = this.MakeNewCharArray();
                }

                this.AddToCharArray(this.charArray, add);
            }

            return this.charArray;
        }

        private char[] MakeNewCharArray()
        {
            char[] newCharArray = new char[this.Capacity * 2];
            newCharArray = this.CopyPaste(newCharArray, this.charArray);
            this.Capacity = newCharArray.Length;
            return newCharArray;
        }

        private char[] CopyPaste(char[] charArray1, char[] charArray2)
        {
            for (int i = 0; i < charArray2.Length; i++)
            {
                charArray1[i] = charArray2[i];
            }

            return charArray1;
        }

        private char[] AddToCharArray(char[] charArray, char[] add)
        {
            for (int i = this.MyLength; i < this.MyLength + add.Length; i++)
            {
                charArray[i] = add[i - this.MyLength];
            }

            this.MyLength += add.Length;
            return charArray;
        }
    }
}
