using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.TextFilesDao
{
    public class UserAwardsDao : IUserAwardsDao
    {
        private IDataAccess dataAccess;

        public UserAwardsDao()
        {
            this.dataAccess = new FileDataAccess();
        }

        public IEnumerable<string> GetUserAwards(User user)
        {

            try
            {
                var userIdAwards = this.dataAccess.GetAllAwardsUsers()
                                        .Select(userId =>
                                        {
                                            if (userId[1] == user.Id)
                                            {
                                                return userId[0];
                                            }
                                            else return -1;
                                        } );

                 var userTitleAwardsList = this.dataAccess.GetAllAwards()
                                                .Select(awardLine =>
                                                {
                                                if (userIdAwards.Contains(awardLine.Id))
                                                {
                                                    return awardLine.Title;
                                                }
                                                else return null;
                                                });

                return userTitleAwardsList;
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        public bool Add(Award award, User user)
        {
            return this.dataAccess.Add(award, user);
        }

        public bool Remove(Award award, User user)
        {
            return this.dataAccess.Remove(award, user);
        }

        public void RemoveUserAwards(int userId)
        {
            this.dataAccess.RemoveUserAwards(userId);
        }

    }
}
