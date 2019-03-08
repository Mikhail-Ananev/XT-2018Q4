using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IMessagesDao
    {
        bool AddMessage(string text, int senderId, int adresseeId);

        bool RemoveMessage(int messageId);

        IEnumerable<Message> GetListLastMessages(int quantity, int senderAccountId, int addresseeAccountId);

        IEnumerable<Message> GetAllAccountMessages(int senderAccountId);

        bool EditMessage(int messageId, string text);

        IEnumerable<Message> GetAllSharedMessages();

        int GetSenderId(int messageId);
    }
}
