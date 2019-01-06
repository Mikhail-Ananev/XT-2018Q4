using System;
using System.Collections.Generic;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class DataAccess : IDataAccess
    {
        public IEnumerable<Award> GetAllAwards()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllUsersAwards()
        {
            throw new NotImplementedException();
        }
    }
}
