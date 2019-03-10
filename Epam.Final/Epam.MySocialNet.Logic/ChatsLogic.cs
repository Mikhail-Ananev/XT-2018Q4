using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;
using Epam.MySocialNet.SQLDao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Logic
{
    public class ChatsLogic : IChatsLogic
    {
        private readonly IChatsDao chatsDao;

        public ChatsLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSN"].ConnectionString;
            this.chatsDao = new ChatsDao(connectionString);
        }


        public IEnumerable<ChatList> GetChatList(int accountId)
        {
            if (accountId < 1)
            {
                return null;
            }

            return chatsDao.GetChatList(accountId);
        }
    }
}
