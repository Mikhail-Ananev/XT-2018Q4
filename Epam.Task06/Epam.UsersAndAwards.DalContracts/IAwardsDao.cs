using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IAwardsDao
    {
        IEnumerable<Award> GetAll();

    }
}
