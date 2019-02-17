using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IUserAwardsDao
    {
        IEnumerable<string> GetUserAwards(int id);

        bool Add(Award award, User user);

        bool UserHasAwards(int userId);

        bool Remove(Award award, User user);

        void RemoveUserAwards(int userId);
    }
}
