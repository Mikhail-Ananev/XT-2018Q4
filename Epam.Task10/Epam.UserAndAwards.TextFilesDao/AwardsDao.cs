using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class AwardsDao : IAwardsDao
    {
        private const string UsersFileName = "users.txt";
        private const string AwardsFileName = "awards.txt";
        private const string FileUsersdAwards = "usersawards.txt";
        private readonly string usersFilePath;
        private readonly string awardsFilePath;
        private readonly string fileAwardsUsersPath;

        //// public var key = ConfigurationManager.AppSettings["AwardsFile"]

        public AwardsDao()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            this.usersFilePath = Path.Combine(folder, UsersFileName);
            this.awardsFilePath = Path.Combine(folder, AwardsFileName);
            this.fileAwardsUsersPath = Path.Combine(folder, FileUsersdAwards);
            //// ConfigurationManager.AppSettings["DAL:Files:Folder"]
            if (!File.Exists(this.fileAwardsUsersPath))
            {
                File.Create(this.fileAwardsUsersPath).Close();
            }
        }

        public IEnumerable<Award> GetAll()
        {
            try
            {
                return File.ReadAllLines(this.awardsFilePath)
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
    }
}
