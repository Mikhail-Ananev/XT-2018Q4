using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IAwardsDao
    {
        IEnumerable<Award> GetAll();

        Award GetAwardById(int awardId);

        bool Add(Award award);

        bool EditTitle(int id, string title);

        bool CheckAwardedUsers(int id);

        bool Remove(int id);

        bool ForcedRemove(int id);
    }
}
