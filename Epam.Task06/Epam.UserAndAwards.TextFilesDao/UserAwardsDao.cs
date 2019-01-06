using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class UserAwardsDao : IUserAwardsDao
    {
        private const string usersFileName = "users.txt";

        private const string awardsFileName = "awards.txt";
        private const string fileUsersdAwards = "usersawards.txt";
        private readonly string awardsFilePath;
        private readonly string usersFilePath;

        private readonly string fileAwardsUsersPath;

        //public var key = ConfigurationManager.AppSettings["AwardsFile"]

        public UserAwardsDao()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            awardsFilePath = Path.Combine(folder, awardsFileName);
            usersFilePath = Path.Combine(folder, usersFileName);

            fileAwardsUsersPath = Path.Combine(folder, fileUsersdAwards);
            //ConfigurationManager.AppSettings["DAL:Files:Folder"]
            if (!File.Exists(fileAwardsUsersPath))
            {
                File.Create(fileAwardsUsersPath).Close();
            }
        }

        public IEnumerable<string> GetUserAwards(User user)
        {
            try
            {
                var userAwards = File.ReadAllLines(fileAwardsUsersPath)
                            .Select(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 2);
                                int userId = int.Parse(parts[1]);
                                return user.Id == userId ? parts[0] : null;
                            });

                 var xxx = File.ReadAllLines(awardsFilePath)
                    .Select(line =>
                    {
                        var parts = line.Split(new[] { '|' }, 2);
                        return userAwards.Contains(parts[0]) ? parts[1] : null;
                    });

                //foreach (var x in xxx)
                //{
                //    Console.WriteLine(x);
                //}

                return xxx;
            }
            catch
            {

                return Enumerable.Empty<string>();
            }
        }

        public bool Add(Award award, User user)
        {
            if (CheckAwardId(award) && CheckUserId(user))
            {
                if (!CheckAwardsUsers(award, user))
                {
                    File.AppendAllLines(fileAwardsUsersPath, new[] { UsersAwardsString(award, user) });
                    return true;
                }
                else
                {
                    Console.WriteLine("User already have this reward");
                    return false;
                }

                //user.AwardExists = true;
                //File.AppendAllLines(fileAwardsUsersPath, new[] { UsersAwardsString(award, user) });
                //return true;
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
            var users = File.ReadAllLines(usersFilePath)
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

            //Console.WriteLine("HERE");
            //foreach (var id in users.Cu)
            //{
            //    Console.WriteLine(id);
            //    if (id == 0)
            //    {
            //        return false;
            //    }
            //}

            //return true;
        }

        private bool CheckAwardId(Award award)
        {
            return File.ReadAllLines(awardsFilePath).Length >= award.Id;
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
            //if (awards == 0)
            //{
            //    RemoveAwardExist(user);
            //}
        }

        //private void RemoveAwardExist(User user)
        //{
        //    user.AwardExists = false;
        //}

        private static string UsersAwardsString(Award award, User user)
        {
            return $"{award.Id}|{user.Id}";
        }

        public void RemoveUserAwards(int userId)
        {
            var awardsUsers = File.ReadAllLines(fileAwardsUsersPath).ToList();
            var lineToRemove = awardsUsers.Where(line => line.Contains("|" + userId.ToString()));
            foreach (var line in lineToRemove)
            {
                awardsUsers.Remove(line);
            }

            File.WriteAllLines(fileAwardsUsersPath, awardsUsers.Select(n => n));
        }
    }
}
