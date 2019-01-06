using System;
using System.Collections.Generic;
using System.Linq;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.TextFilesDao;

namespace Epam.UsersAndAwards.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUsersDao usersDao;

        public UserLogic()
        {
            ////через if реализовать выбор из файла конфигурации
            this.usersDao = new TextFilesDao.UsersDao();
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

            User user = new User { FirstName = firstName, LastName = lastName, BirthDate = birthDate, Age = age };
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