using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            User user = new User();

            Console.WriteLine("Input first name:");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Input middle name:");
            user.MiddleName = Console.ReadLine();
            Console.WriteLine("Input last name:");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Input age:");
            int n;
            bool result = int.TryParse(Console.ReadLine(), out n);
            if (!result)
            {
                throw new ArgumentException("Incorrect age!", nameof(n));
            }

            user.Age = n;
            Console.WriteLine($"User full name: {user.FirstName} {user.MiddleName} {user.LastName}{Environment.NewLine}" +
                              $"User age:       {user.Age}");
        }
    }
}