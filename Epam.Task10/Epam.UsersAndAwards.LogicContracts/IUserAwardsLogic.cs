﻿using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IUserAwardsLogic
    {
        IEnumerable<string> GetUserAwards(int id);

        bool Add(int awardId, int userId);

        bool Remove(int awardId, int userId);

        bool UserHasAwards(int userId);

        void RemoveUserAwards(int id);
    }
}
