﻿using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IMessagesLogic
    {
        bool AddMessage(string text, int senderId, int adresseeId);

        bool RemoveMessage(int accountId, int messageId, string accountRole);

        IEnumerable<Message> GetListLastMessages(int quantity, int senderAccountId, int addresseeAccountId);

        IEnumerable<Message> GetAllAccountMessages(int senderAccountId);

        bool EditMessage(int accountId, int messageId, string accountRole, string text);

        IEnumerable<Message> GetAllSharedMessages();
    }
}
