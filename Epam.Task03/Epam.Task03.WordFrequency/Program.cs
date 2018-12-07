using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task03.WordFrequency
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            string inputText = "These brothers bathe with those brothers " +
                               "Those brothers bathe with these brothers " +
                               "If these brothers didn’t bathe with those brothers " +
                               "Would those brothers bathe with these brothers ".ToLower();
            char[] separator = { '.', ' ' };
            Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            int count = 0;
            foreach (string str in inputText.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                if (dictionary.ContainsKey(str))
                {
                    dictionary[str]++;
                }
                else
                {
                    dictionary.Add(str, 1);
                    count++;
                }
            }

            Console.WriteLine(inputText);
            Console.WriteLine($"Words count: {count}");
            foreach (var s in dictionary)
            {
                Console.WriteLine($"The word '{s.Key}' repeated {s.Value} times");
            }
        }
    }
}
