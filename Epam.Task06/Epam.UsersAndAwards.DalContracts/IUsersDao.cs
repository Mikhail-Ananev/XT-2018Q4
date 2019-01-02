using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public interface IUsersDao
    {
        void Add(User user);

        bool Remove(int id);

        IEnumerable<User> GetAll();
    }
}
