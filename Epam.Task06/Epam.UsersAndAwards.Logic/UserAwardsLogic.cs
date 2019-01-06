using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.Logic
{
    public class UserAwardsLogic : IUserAwardsLogic
    {
        private readonly IUserAwardsDao userAwardsDao;

        public UserAwardsLogic()
        {
            //через if реализовать выбор из файла конфигурации
            this.userAwardsDao = new TextFilesDao.UserAwardsDao();
        }

        public IEnumerable<string> GetUserAwards(User user)
        {
            var xx = this.userAwardsDao.GetUserAwards(user);
            //foreach (var x in xx)
            //{
            //    Console.WriteLine(x);
            //}
            return this.userAwardsDao.GetUserAwards(user);
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
            userAwardsDao.RemoveUserAwards(id);
        }
    }
}
