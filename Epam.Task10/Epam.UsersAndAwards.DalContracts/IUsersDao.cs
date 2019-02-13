using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public interface IUsersDao
    {
        void Add(User user);

        bool Remove(int id);

        IEnumerable<User> GetAll();

        void EditUser(User user);

        User GetUserById(int id);

    }
}
