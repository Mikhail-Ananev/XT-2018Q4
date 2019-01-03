using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class AwardsDao : IAwardsDao
    {
        private const string usersFileName = "users.txt";
        private const string awardsFileName = "awards.txt";
        private const string fileUsersdAwards = "usersawards.txt";
        private readonly string usersFilePath;
        private readonly string awardsFilePath;
        private readonly string fileAwardsUsersPath;

        //public var key = ConfigurationManager.AppSettings["AwardsFile"]

        public AwardsDao()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            usersFilePath = Path.Combine(folder, usersFileName);
            awardsFilePath = Path.Combine(folder, awardsFileName);
            fileAwardsUsersPath = Path.Combine(folder, fileUsersdAwards);
            //ConfigurationManager.AppSettings["DAL:Files:Folder"]
            if (!File.Exists(fileAwardsUsersPath))
            {
                File.Create(fileAwardsUsersPath).Close();
            }
        }

        private static string UsersAwardsString(Award award, User user)
        {
            return $"{award.Id}|{user.Id}";
        }

        public IEnumerable<Award> GetAll()
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

                //var xxx = File.ReadAllLines(awardsFilePath);
                //foreach (var x in xxx)
                //{
                //    Console.WriteLine(x);
                //}
                //return Enumerable.Empty<Award>();

            }
            catch
            {
                return Enumerable.Empty<Award>();
            }
        }

        public bool Add(Award award, User user)
        {
            if (CheckAwardId(award) && CheckUserId(user))
            {
                if (!CheckAwardsUsers(award, user))
                {
                    File.AppendAllLines(fileAwardsUsersPath, new[] { UsersAwardsString(award, user) });
                }

                user.AwardExists = true;
                return true;
            }
            else
            {
                Console.WriteLine("One or both ID incorrect");
                return false;
            }
        }

        private bool CheckAwardsUsers(Award award, User user)
        {
            var awardsUsers = File.ReadAllLines(fileAwardsUsersPath)
                            .Where(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 2);
                                return parts[0] == award.Id.ToString() && parts[1] == user.Id.ToString();
                            });
            if (awardsUsers == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckUserId(User user)
        {
            var users = File.ReadAllLines(usersFilePath)
                            .Select(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 1);
                                return int.Parse(parts[0]);
                            })
                            .Where(id => id == user.Id);
            if (users == null)
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
            return File.ReadAllLines(awardsFilePath).Length > award.Id;
        }

        public bool Remove(Award award, User user)
        {
            var awardsUsers = File.ReadAllLines(fileAwardsUsersPath).ToList();
            string search = string.Concat(award.Id.ToString() + "|" + user.Id.ToString());
            var result = awardsUsers.FirstOrDefault(n => n == search);
            if (result == null)
            {
                return false;
            }

            CheckExistsUserAwards(user);
            awardsUsers.Remove(result);
            File.WriteAllLines(fileAwardsUsersPath, awardsUsers.Select(n => n));
            return true;
        }

        private void CheckExistsUserAwards(User user)
        {
            var awards = File.ReadAllLines(fileAwardsUsersPath)
                            .Select(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 2);
                                return int.Parse(parts[1]);
                            })
                            .Where(id => id == user.Id)
                            .Count();
            if (awards == 0)
            {
                RemoveAwardExist(user);
            }
        }

        private void RemoveAwardExist(User user)
        {
            user.AwardExists = false;
        }
    }
}
