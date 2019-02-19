using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.SQLDao;

namespace Epam.UsersAndAwards.Logic
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IAwardsDao awardsDao;

        public AwardLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UaADB"].ConnectionString;
            this.awardsDao = new SQLAwardsDao(connectionString);
        }

        public Award GetAwardById(int awardId)
        {
            return this.awardsDao.GetAwardById(awardId);
        }

        public bool SaveNewAward(Award award)
        {
            if (award == null)
            {
                throw new ArgumentNullException($"{nameof(award)}");
            }

            if (string.IsNullOrWhiteSpace(award.Title))
            {
                throw new ArgumentException("Name text cannot be null or whitespace");
            }

            if (awardsDao.Add(award))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Award> GetAll()
        {
            return this.awardsDao.GetAll().ToArray();
        }

        public bool EditTitle(int id, string title)
        {
            return this.awardsDao.EditTitle(id, title);
        }

        public bool Remove(int id)
        {
            if (!awardsDao.CheckAwardedUsers(id))
            {
                return awardsDao.Remove(id);
            }
            else
            {
            return false;
            }
        }

        public bool ForcedRemove(int id)
        {
            return this.awardsDao.ForcedRemove(id);
        }

        public bool CheckAwardedUsers(int id)
        {
            return awardsDao.CheckAwardedUsers(id);
        }
    }
}
