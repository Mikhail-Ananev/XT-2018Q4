using System.Collections.Generic;
using System.Configuration;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.SQLDao;

namespace Epam.UsersAndAwards.Logic
{
    public class UserAwardsLogic : IUserAwardsLogic
    {
        private readonly IUserAwardsDao userAwardsDao;

        public UserAwardsLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UaADB"].ConnectionString;
            this.userAwardsDao = new SQLUserAwardsDao(connectionString);
        }

        public IEnumerable<string> GetUserAwards(int id)
        {
            return this.userAwardsDao.GetUserAwards(id);
        }

        public bool Add(int awardId, int userId)
        {
            Award award = new Award { Id = awardId };
            User user = new User { Id = userId };
            return this.userAwardsDao.Add(award, user);
        }

        public bool Remove(int awardId, int userId)
        {
            Award award = new Award { Id = awardId };
            User user = new User { Id = userId };
            return this.userAwardsDao.Remove(award, user);
        }

        public void RemoveUserAwards(int id)
        {
            this.userAwardsDao.RemoveUserAwards(id);
        }

        public bool UserHasAwards(int userId)
        {
            return this.userAwardsDao.UserHasAwards(userId);
        }
    }
}
