using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IAwardLogic
    {
        IEnumerable<Award> GetAll();

    }
}
