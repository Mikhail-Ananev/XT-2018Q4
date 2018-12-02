using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyString
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            MyString myString = new MyString("First string".ToCharArray());
            MyString.Show(myString);
            Console.WriteLine($"myString.Capacity: {myString.Capacity}{Environment.NewLine}" +
                $"myString.MyLength: {myString.MyLength}{Environment.NewLine}");
            myString.Append(" after Append.".ToCharArray());
            MyString.Show(myString);
            Console.WriteLine($"myString.Capacity: {myString.Capacity}{Environment.NewLine}" +
                $"myString.MyLength: {myString.MyLength}{Environment.NewLine}");
            char[] charString = MyString.Concat("Concat1".ToCharArray(), "Concat2".ToCharArray());
            MyString.Show(charString);
            charString = MyString.Concat("Concat01".ToCharArray(), "Concat02".ToCharArray(), "Concat03".ToCharArray());
            MyString.Show(charString);
            myString = MyString.CharArrayToMyString(charString);
            Console.WriteLine("Result convertation char array to myString:");
            MyString.Show(myString);
            charString = MyString.MyStringToCharArray(myString);
            Console.WriteLine("Result convertation myString to char array:");
            MyString.Show(charString);
            Console.WriteLine("\"This text\" and \"this text\" equals: {0}", MyString.MyEqual("This text".ToCharArray(), "this text".ToCharArray()));
            Console.WriteLine("\"This text\" and \"This text\" equals: {0}", MyString.MyEqual("This text".ToCharArray(), "This text".ToCharArray()));
            Console.WriteLine();
            charString = "In this string first index for 'i' and last index for 'y' symbols is".ToCharArray();
            MyString.Show(charString);
            Console.WriteLine($"First index 'i':{MyString.FirstIndex(charString, 'i')}{Environment.NewLine}" +
                              $"Last index 'y':{MyString.LastIndex(charString, 'y')}");
        }
    }
}