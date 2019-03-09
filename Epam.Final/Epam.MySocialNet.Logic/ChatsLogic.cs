using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Logic
{
    class ChatsLogic : IChatsLogic
    {
        private readonly IChatsDao imageDao;

        public ChatsLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSN"].ConnectionString;
            this.chatDao = new ChatsDao(connectionString);
        }


        public IEnumerable<ChatList> GetChatList(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
