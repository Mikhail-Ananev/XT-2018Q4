using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class UserAwardsDao : IUserAwardsDao
    {
        private const string UsersFileName = "users.txt";
        private const string AwardsFileName = "awards.txt";
        private const string FileUsersdAwards = "usersawards.txt";
        private readonly string awardsFilePath;
        private readonly string usersFilePath;
        private readonly string fileAwardsUsersPath;

        ////public var key = ConfigurationManager.AppSettings["AwardsFile"]

        public UserAwardsDao()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            this.awardsFilePath = Path.Combine(folder, AwardsFileName);
            this.usersFilePath = Path.Combine(folder, UsersFileName);
            this.fileAwardsUsersPath = Path.Combine(folder, FileUsersdAwards);
            if (!File.Exists(this.fileAwardsUsersPath))
            {
                File.Create(this.fileAwardsUsersPath).Close();
            }
        }

        public IEnumerable<string> GetUserAwards(int id)
        {
            try
            {
                var userAwards = File.ReadAllLines(this.fileAwardsUsersPath)
                            .Select(line =>
                            {
                                var parts = line.Split(new[] { '|' }, 2);
                                int userId = int.Parse(parts[1]);
                                return id == userId ? parts[0] : null;
                            });

                 var xxx = File.ReadAllLines(this.awardsFilePath)
                    .Select(line =>
                    {
                        var parts = line.Split(new[] { '|' }, 2);
                        return userAwards.Contains(parts[0]) ? parts[1] : null;
                    });

                return xxx;
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
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
            var awardsUsers = File.ReadAllLines(this.fileAwardsUsersPath).ToList();
            var lineToRemove = awardsUsers.Where(line => line.Contains("|" + userId.ToString()));
            foreach (var line in lineToRemove)
            {
                awardsUsers.Remove(line);
            }

            File.WriteAllLines(this.fileAwardsUsersPath, awardsUsers.Select(n => n));
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
    }
}
