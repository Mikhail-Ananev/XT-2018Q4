using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IDataAccess
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<Award> GetAllAwards();

        IEnumerable<string> GetAllUsersAwards();
    }
}
