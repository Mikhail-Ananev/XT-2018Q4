using System.Collections.Generic;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class UsersDao : IUsersDao
    {
        private IDataAccess dataAccess;

        public UsersDao()
        {
            this.dataAccess = new FileDataAccess();
        }

        public void Add(User user)
        {
            this.dataAccess.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return this.dataAccess.GetAllUsers();
        }

        public bool Remove(int id)
        {
            return this.dataAccess.RemoveUser(id);
        }
    }
}
