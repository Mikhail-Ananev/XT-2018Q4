using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class UsersDao : IUsersDao
    {
        private const string UsersFileName = "users.txt";
        private const string MaxIdFileName = "maxId.txt";
        private const string DateFormat = "yyyyMMddHHmmss";
        private readonly string usersFilePath;
        private readonly string maxIdFilePath;
        private int maxId;

        public UsersDao()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            this.usersFilePath = Path.Combine(folder, UsersFileName);
            this.maxIdFilePath = Path.Combine(folder, MaxIdFileName);
            ////ConfigurationManager.ArrSettings["DAL:Files:Folder"]
            try
            {
                this.maxId = int.Parse(File.ReadAllText(this.maxIdFilePath));
            }
            catch
            {
                this.maxId = 0;
            }
        }

        public void Add(User user)
        {
            user.Id = ++this.maxId;
            File.WriteAllText(this.maxIdFilePath, this.maxId.ToString());
            File.AppendAllLines(this.usersFilePath, new[] { UserDataString(user) });
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return File.ReadAllLines(this.usersFilePath)
                            .Select(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 4);
                                return new User
                                {
                                    Id = int.Parse(parts[0]),
                                    FirstName = parts[1],
                                    LastName = parts[2],
                                    BirthDate = DateTime.ParseExact(parts[3], DateFormat, CultureInfo.InvariantCulture),
                                };
                            });
            }
            catch
            {
                return Enumerable.Empty<User>();
            }
        }

        public bool Remove(int id)
        {
            var users = this.GetAll().ToList();
            var user = users.FirstOrDefault(n => n.Id == id);
            if (user == null)
            {
                return false;
            }

            users.Remove(user);
            File.WriteAllLines(this.usersFilePath, users.Select(UserDataString));
            return true;
        }

        private static string UserDataString(User user)
        {
            return $"{user.Id}|{user.FirstName}|{user.LastName}|{ user.BirthDate.ToString(DateFormat)}";
        }

        public void EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
