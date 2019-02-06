using System.Collections.Generic;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;

namespace Epam.UsersAndAwards.Logic
{
    public class UserAwardsLogic : IUserAwardsLogic
    {
        private readonly IUserAwardsDao userAwardsDao;

        public UserAwardsLogic()
        {
            ////через if реализовать выбор из файла конфигурации
            this.userAwardsDao = new TextFilesDao.UserAwardsDao();
        }

        public IEnumerable<string> GetUserAwards(int id)
        {
            var xx = this.userAwardsDao.GetUserAwards(id);
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
    }
}
