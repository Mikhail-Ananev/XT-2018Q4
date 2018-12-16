using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task04.CustomSort
{
    public class SortMethod
    {
        public static int CompareByLength(string str1, string str2)
        {
            if (str1 == null || str2 == null)
            {
                throw new ArgumentException("One or both parameters 'null'!");
            }

            if (str1.Length == str2.Length)
            {
                return 0;
            }

            if (str1.Length > str2.Length)
            {
                return 1;
            }

            return -1;
        }

        public static int CompareByChar(string str1, string str2)
        {
            int minLength = SortMethod.CompareByLength(str1, str2) < 0 ? str1.Length : str2.Length;
            for (int i = 0; i < minLength; i++)
            {
                if (str1[i] != str2[i])
                {
                    if (str1[i] > str2[i])
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }
    }
}
