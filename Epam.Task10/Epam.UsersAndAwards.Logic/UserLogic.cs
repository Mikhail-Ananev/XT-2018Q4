using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.SQLDao;
using Epam.UsersAndAwards.TextFilesDao;

namespace Epam.UsersAndAwards.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUsersDao usersDao;

        public UserLogic()
        {
            ////через if реализовать выбор из файла конфигурации
            ////this.usersDao = new UsersDao();
            string connectionString = ConfigurationManager.ConnectionStrings["UaADB"].ConnectionString;
            this.usersDao = new SQLUsersDao(connectionString);
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

            int age = GetAge(birthDate);
            if (age < 4 || age > 125)
            {
                Console.WriteLine("Incorrect input birthday");
                return false;
            }

            User user = new User { FirstName = firstName, LastName = lastName, BirthDate = birthDate};
            try
            {
                this.usersDao.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<User> GetAll()
        {
            return this.usersDao.GetAll().ToArray();
        }

        public bool Remove(int id)
        {
            return this.usersDao.Remove(id);
        }

        public int GetAge(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (birthday > now.AddYears(-age)) age--;

            return age;
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

        public bool EditUser(string strId, string firstName, string lastName, string stringBirthDate)
        {
            int id;
            if (!int.TryParse(strId, out id))
            {
                return false;
            }

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

            int age = GetAge(birthDate);
            if (age < 4 || age > 125)
            {
                Console.WriteLine("Incorrect input birthday");
                return false;
            }

            User user = new User { Id = id, FirstName = firstName, LastName = lastName, BirthDate = birthDate };
            try
            {
                this.usersDao.EditUser(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUserById(int id)
        {
            return this.usersDao.GetUserById(id);
        }

    }
}