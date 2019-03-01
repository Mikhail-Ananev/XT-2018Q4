using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IMessagesLogic
    {
        bool AddMessage(Message message);

        bool RemoveMessage(int messageId);

        IEnumerable<Message> GetListLastMessages(int quantity, int addresseeAccountId);

        IEnumerable<Message> GetAllAccountMessages(int senderAccountId);

        bool EditMessage(int accountId, int messageId, string accountRole);
    }
}
