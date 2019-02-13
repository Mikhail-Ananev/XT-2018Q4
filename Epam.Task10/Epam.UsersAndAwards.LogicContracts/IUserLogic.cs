using System;
using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();

        bool Add(string firstName, string lastName, string stringBirthDate);

        bool Remove(int id);

        int GetAge(DateTime birthday);

        bool EditUser(string id, string firstName, string lastName, string stringBirthDate);

        User GetUserById(int id);
    }
}
