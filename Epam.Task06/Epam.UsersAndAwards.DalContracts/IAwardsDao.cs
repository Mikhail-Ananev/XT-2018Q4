using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IAwardsDao
    {
        IEnumerable<Award> GetAll();

        bool Add(Award award, User user);

        bool Remove(Award award, User user);
    }
}
