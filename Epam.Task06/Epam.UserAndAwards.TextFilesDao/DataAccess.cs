using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.TextFilesDao
{
    class DataAccess : IDataAccess
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
