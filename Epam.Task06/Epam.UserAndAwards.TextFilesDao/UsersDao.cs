using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class UsersDao : IUsersDao
    {
        // 1|12312323123|textttttttttttttttt
        private const string usersFileName = "users.txt";
        private const string maxIdFileName = "maxId.txt";
        private const string DateFormat = "yyyyMMddHHmmss";
        //private readonly string folder;
        private readonly string usersFilePath;
        private readonly string maxIdFilePath;
        private int maxId;

        public UsersDao()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            usersFilePath = Path.Combine(folder, usersFileName);
            maxIdFilePath = Path.Combine(folder, maxIdFileName);

            //ConfigurationManager.ArrSettings["DAL:Files:Folder"]
            try
            {
                maxId = int.Parse(File.ReadAllText(maxIdFilePath));
            }
            catch
            {
                maxId = 0;
            }
        }

        public void Add(User user)
        {
            user.Id = ++maxId;
            File.WriteAllText(maxIdFilePath, maxId.ToString());
            File.AppendAllLines(usersFilePath, new[] { UserDataString(user) });
        }

        private static string UserDataString(User user)
        {
            return $"{user.Id}|{user.FirstName}|{user.LastName}|{ user.BirthDate.ToString(DateFormat)}|{user.Age}";
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return File.ReadAllLines(usersFilePath)
                    .Select(line =>
                    {
                        var parts = line.Split(new[] { '|' }, 5);
                        return new User
                        {
                            Id = int.Parse(parts[0]),
                            FirstName = parts[1],
                            LastName = parts[2],
                            BirthDate = DateTime.ParseExact(parts[3], DateFormat, CultureInfo.InvariantCulture),
                            Age = int.Parse(parts[4]),
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
            var users = GetAll().ToList();
            var user = users.FirstOrDefault(n => n.Id == id);
            if (user == null)
            {
                return false;
            }

            users.Remove(user);
            File.WriteAllLines(usersFilePath, users.Select(UserDataString));
            return true;
        }

    }
}
