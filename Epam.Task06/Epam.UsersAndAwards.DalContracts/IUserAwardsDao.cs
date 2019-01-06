using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IUserAwardsDao
    {
        IEnumerable<string> GetUserAwards(User user);

        bool Add(Award award, User user);

        bool Remove(Award award, User user);

        void RemoveUserAwards(int userId);
    }
}
