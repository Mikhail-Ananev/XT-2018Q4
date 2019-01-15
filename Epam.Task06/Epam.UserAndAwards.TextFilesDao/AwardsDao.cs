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
        private IDataAccess dataAccess;
        private const string AwardsFileName = "awards.txt";
        private readonly string awardsFilePath;

        //// public var key = ConfigurationManager.AppSettings["AwardsFile"]

        public AwardsDao()
        {
            dataAccess = new FileDataAccess();
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            this.awardsFilePath = Path.Combine(folder, AwardsFileName);
        }

        public IEnumerable<Award> GetAll()
        {
            return this.dataAccess.GetAllAwards();
        }
    }
}
