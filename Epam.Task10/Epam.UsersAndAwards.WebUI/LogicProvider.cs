using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.UsersAndAwards.WebUI
{
    public static class LogicProvider
    {
        static LogicProvider()
        {
            AwardLogic = new AwardLogic();
            UserAwardsLogic = new UserAwardsLogic();
            UserLogic = new UserLogic();
            AccountsLogic = new AccountsLogic();
        }

        public static IAwardLogic AwardLogic { get; }

        public static IUserAwardsLogic UserAwardsLogic { get; }

        public static IUserLogic UserLogic { get; }

        public static IAccountsLogic AccountsLogic { get; }
    }
}