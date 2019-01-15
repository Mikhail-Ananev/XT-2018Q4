using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{

    public class FileDataAccess : IDataAccess
    {

        private const string DateFormat = "yyyyMMddHHmmss"; //в AppConfig
        private const string UsersFileName = "users.txt";//в AppConfig
        private const string MaxIdFileName = "maxId.txt";//в AppConfig
        private const string AwardsFileName = "awards.txt";
        private const string FileAwardsUsers = "awardsusers.txt";

        private readonly string usersFilePath;//в AppConfig
        private readonly string maxIdFilePath;//в AppConfig
        private readonly string awardsFilePath;
        private readonly string fileAwardsUsersPath;

        private int maxId;

        public FileDataAccess()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            this.usersFilePath = Path.Combine(folder, UsersFileName);//в AppConfig
            this.maxIdFilePath = Path.Combine(folder, MaxIdFileName);//в AppConfig
            this.awardsFilePath = Path.Combine(folder, AwardsFileName);
            this.fileAwardsUsersPath = Path.Combine(folder, FileAwardsUsers);

            try
            {
                this.maxId = int.Parse(File.ReadAllText(this.maxIdFilePath));
            }
            catch
            {
                this.maxId = 0;
            }

            if (!File.Exists(this.fileAwardsUsersPath))
            {
                File.Create(this.fileAwardsUsersPath).Close();
            }

        }


        ////public var key = ConfigurationManager.AppSettings["AwardsFile"]




        public IEnumerable<Award> GetAllAwards()
        {
            try
            {
                return File.ReadAllLines(awardsFilePath)
                    .Select(line =>
                    {
                        var parts = line.Split(new[] { '|' }, 2);
                        return new Award
                        {
                            Id = int.Parse(parts[0]),
                            Title = parts[1],
                        };
                    });
            }
            catch
            {
                return Enumerable.Empty<Award>();
            }
        }

        public IEnumerable<int[]> GetAllAwardsUsers()
        {
            return File.ReadAllLines(fileAwardsUsersPath)
                .Select(line =>
                {
                    var parts = line.Split(new[] { '|' }, 2);
                    return new int[]
                    {
                        int.Parse(parts[0]),
                        int.Parse(parts[1]),
                    };
                });
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return File.ReadAllLines(this.usersFilePath)
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

        public void Add(User user)
        {
            user.Id = ++this.maxId;
            File.WriteAllText(this.maxIdFilePath, this.maxId.ToString());
            File.AppendAllLines(this.usersFilePath, new[] { UserDataString(user) });
        }

        public bool RemoveUser(int id)
        {
            var users = this.GetAllUsers().ToList();
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
            return $"{user.Id}|{user.FirstName}|{user.LastName}|{ user.BirthDate.ToString(DateFormat)}|{user.Age}";
        }

        public bool Add(Award award, User user)
        {
            if (this.CheckAwardId(award) && this.CheckUserId(user))
            {
                if (!this.CheckAwardsUsers(award, user))
                {
                    File.AppendAllLines(this.fileAwardsUsersPath, new[] { this.UsersAwardsString(award, user) });
                    return true;
                }
                else
                {
                    Console.WriteLine("User already have this reward");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("One or both ID incorrect");
                return false;
            }
        }
        private bool CheckAwardsUsers(Award award, User user)
        {
            var awardsUsers = File.ReadAllLines(this.fileAwardsUsersPath)
                            .Where(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 2);
                                return parts[0] == award.Id.ToString() && parts[1] == user.Id.ToString();
                            });
            if (awardsUsers.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckUserId(User user)
        {
            var users = File.ReadAllLines(this.usersFilePath)
                            .Select(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 5);
                                return int.Parse(parts[0]);
                            })
                            .Where(id => id == user.Id);

            if (users.SingleOrDefault() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckAwardId(Award award)
        {
            return File.ReadAllLines(this.awardsFilePath).Length >= award.Id;
        }

        private void CheckExistsUserAwards(User user)
        {
            var awards = File.ReadAllLines(this.fileAwardsUsersPath)
                            .Select(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 2);
                                return int.Parse(parts[1]);
                            })
                            .Where(id => id == user.Id)
                            .Count();
        }

        private string UsersAwardsString(Award award, User user)
        {
            return $"{award.Id}|{user.Id}";
        }

        public bool Remove(Award award, User user)
        {
            var awardsUsers = File.ReadAllLines(this.fileAwardsUsersPath).ToList();
            string search = string.Concat(award.Id.ToString() + "|" + user.Id.ToString());
            var result = awardsUsers.FirstOrDefault(n => n == search);
            if (result == null)
            {
                return false;
            }

            this.CheckExistsUserAwards(user);
            awardsUsers.Remove(result);
            File.WriteAllLines(this.fileAwardsUsersPath, awardsUsers.Select(n => n));
            return true;
        }

        public void RemoveUserAwards(int userId)
        {
            var awardsUsers = GetAllAwardsUsers().Select(line =>
            {
                return line[1] != userId ? line[0].ToString() + "|" +line[0].ToString() : null;
            });
            //var lineToRemove = awardsUsers;
                                
            //foreach (var line in lineToRemove)
            //{
            //    awardsUsers.Remove(line);
            //}

            File.WriteAllLines(this.fileAwardsUsersPath, awardsUsers);
        }

    }
}
//static void ReadSetting(string key)
//{
//    try
//    {
//        var appSettings = ConfigurationManager.AppSettings;
//        string result = appSettings[key] ?? "Not Found";
//        Console.WriteLine(result);
//    }
//    catch (ConfigurationErrorsException)
//    {
//        Console.WriteLine("Error reading app settings");
//    }
//}
//        <configuration>  
//    <startup>   
//        <supportedRuntime version = "v4.0" sku=".NETFramework,Version=v4.5" />  
//    </startup>  
//  <appSettings>  
//    <add key = "Setting1" value="May 5, 2014"/>  
//    <add key = "Setting2" value="May 6, 2014"/>  
//  </appSettings>  
//</configuration>  
