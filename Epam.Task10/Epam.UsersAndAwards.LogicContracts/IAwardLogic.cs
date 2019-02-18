using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IAwardLogic
    {
        IEnumerable<Award> GetAll();

        Award GetAwardById(int awardId);

        bool SaveNewAward(Award award);

        bool EditTitle(int id, string title);

        bool Remove(int id);

        bool ForcedRemove(int id);
    }
}
