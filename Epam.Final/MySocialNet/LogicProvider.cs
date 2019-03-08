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
            ImagesLogic = new ImagesLogic();
            MessagesLogic = new MessagesLogic();
        }

        public static IAccountsLogic AccountsLogic { get; }

        public static IImagesLogic ImagesLogic { get; }

        public static IMessagesLogic MessagesLogic { get; }
    }
}