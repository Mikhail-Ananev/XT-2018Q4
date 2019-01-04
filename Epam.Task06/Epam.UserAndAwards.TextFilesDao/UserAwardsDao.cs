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
        private const string awardsFileName = "awards.txt";
        private const string fileUsersdAwards = "usersawards.txt";
        private readonly string awardsFilePath;
        private readonly string fileAwardsUsersPath;

        //public var key = ConfigurationManager.AppSettings["AwardsFile"]

        public UserAwardsDao()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            awardsFilePath = Path.Combine(folder, awardsFileName);
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
    }
}
