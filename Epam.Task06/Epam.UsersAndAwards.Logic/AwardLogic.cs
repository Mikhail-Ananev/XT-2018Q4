using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.TextFilesDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.Logic
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IAwardsDao awardsDao;

        public AwardLogic()
        {
            //через if реализовать выбор из файла конфигурации
            this.awardsDao = new TextFilesDao.AwardsDao();
        }

        public bool Add(int awardId, int userId)
        {
            Award award = new Award { Id = awardId };
            User user = new User { Id = userId };
            return this.awardsDao.Add(award, user);
        }

        public bool Remove(int awardId, int userId)
        {
            Award award = new Award { Id = awardId };
            User user = new User { Id = userId };
            return this.awardsDao.Remove(award, user);
        }

        IEnumerable<Award> IAwardLogic.GetAll()
        {
            return this.awardsDao.GetAll().ToArray();
        }
    }
}
