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
            return this.userAwardsDao.GetUserAwards(user);
        }

    }
}
