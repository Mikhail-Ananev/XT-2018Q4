using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IDataAccess
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<Award> GetAllAwards();

        IEnumerable<int[]> GetAllAwardsUsers();

        void Add(User user);

        bool RemoveUser(int id);

        bool Add(Award award, User user);

        bool Remove(Award award, User user);

        void RemoveUserAwards(int userId);


    }
}
