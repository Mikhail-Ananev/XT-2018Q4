using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IAwardLogic
    {
        IEnumerable<Award> GetAll();

        bool Add(int awardId, int userId);

        bool Remove(int awardId, int userId);
    }
}
