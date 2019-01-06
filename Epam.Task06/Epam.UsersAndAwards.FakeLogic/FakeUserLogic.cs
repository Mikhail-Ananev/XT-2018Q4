using System;
using System.Collections.Generic;
using System.Linq;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;

namespace Epam.UsersAndAwards.FakeLogic
{
    public class FakeUserLogic : IUserLogic
    {
        private readonly ICollection<User> users;
        private int maxId;

        public FakeUserLogic()
        {
            this.users = new HashSet<User>();
        }

        public bool Add(string firstName, string lastName, string stringBirthDate)
        {
            if (!this.Check(firstName))
            {
                return false; 
            }

            if (!this.Check(lastName))
            {
                return false;
            }

            DateTime birthDate;
            try
            {
                birthDate = DateTime.Parse(stringBirthDate);
            }
            catch
            {
                Console.WriteLine("Incorrect input birthday");
                return false;
            }

            int nowDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int birstDay = int.Parse(birthDate.ToString("yyyyMMdd"));
            int age = (nowDate - birstDay) / 10000;
            if (age < 4 || age > 125)
            {
                Console.WriteLine("Incorrect input birthday");
                return false;
            }

            User user = new User { Id = ++this.maxId, FirstName = firstName, LastName = lastName, BirthDate = birthDate, Age = age };
            this.users.Add(user);
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return this.users.Select(n => n);
        }

        public bool Remove(int id)
        {
            User user = this.users.FirstOrDefault(n => n.Id == id);
            if (user != null)
            {
                this.users.Remove(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Check(string str)
        {
            if (str == null)
            {
                Console.WriteLine("User first or last name must contains symbols!");
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                {
                    Console.WriteLine("User name must contain only letters!");
                    return false;
                }
            }

            if (!char.IsUpper(str[0]))
            {
                Console.WriteLine("The first symbol user first or last name must be upper!");
                return false;
            }

            return true;
        }
    }
}