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

        public bool EditAwards(int userId, IEnumerable<int> newListAwardsId)
        {
            var userAwards = new List<int>();
            foreach (var awardId in GetUserAwards(userId))
            {
                userAwards.Add(int.Parse(awardId));
            }

            foreach (var newAwardId in newListAwardsId)
            {
                if (!userAwards.Contains(newAwardId))
                {
                    if (!this.userAwardsDao.Add(new Award { Id = newAwardId }, new User { Id = userId }))
                    {
                        return false;
                    }
                }
                else
                {
                    userAwards.Remove(newAwardId);
                }
            }

            foreach (var removedAward in userAwards)
            {
                if (!this.userAwardsDao.Remove(new Award { Id = removedAward }, new User { Id = userId }))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
