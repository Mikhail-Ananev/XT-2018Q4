using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IUserAwardsLogic
    {
        IEnumerable<string> GetUserAwards(User user);
    }
}
