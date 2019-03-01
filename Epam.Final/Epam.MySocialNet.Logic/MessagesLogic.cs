using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Logic
{
    class MessagesLogic : IMessagesLogic
    {
        public bool AddMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public bool EditMessage(int accountId, int messageId, string accountRole)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetAllAccountMessages(int senderAccountId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetListLastMessages(int quantity, int addresseeAccountId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveMessage(int messageId)
        {
            throw new NotImplementedException();
        }
    }
}
