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

    }
}
