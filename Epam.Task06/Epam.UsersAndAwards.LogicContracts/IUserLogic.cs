using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();

        bool Add(string firstName, string lastName, string stringBirthDate);

        bool Remove(int id);
    }
}
