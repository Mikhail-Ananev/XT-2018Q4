using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var employee1 = new Employee()
            {
                FirstName = "John",
                LastName = "Smith",
                MiddleName = "Semenovich",
                Age = 65,
                Position = "Director",
                Experience = 30,
            };
            var employee2 = new Employee()
            {
                FirstName = "John",
                LastName = "Smith",
                MiddleName = "Junior",
                Age = 28,
                Position = "Butcher",
                Experience = 8,
            };

            Console.WriteLine($"FirstName: {employee1.FirstName}{Environment.NewLine}" +
                $"LastName: {employee1.LastName}{Environment.NewLine}" +
                $"MiddleName: {employee1.MiddleName}{Environment.NewLine}" +
                $"Age: {employee1.Age}{Environment.NewLine}" +
                $"Position: {employee1.Position}{Environment.NewLine}" +
                $"Experience: {employee1.Experience}");

            Console.WriteLine();

            Console.WriteLine($"FirstName: {employee2.FirstName}{Environment.NewLine}" +
                $"LastName: {employee2.LastName}{Environment.NewLine}" +
                $"MiddleName: {employee2.MiddleName}{Environment.NewLine}" +
                $"Age: {employee2.Age}{Environment.NewLine}" +
                $"Position: {employee2.Position}{Environment.NewLine}" +
                $"Experience: {employee2.Experience}");
        }
    }
}
