using Epam.MySocialNet.LogicContracts;
using Epam.MySocialNet.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.MySocialNet.WebUI
{
    public static class LogicProvider
    {
        static LogicProvider()
        {
            AccountsLogic = new AccountsLogic();
        }

        public static IAccountsLogic AccountsLogic { get; }
    }
}