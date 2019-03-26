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
    public class MessagesLogic : IMessagesLogic
    {
        private IMessagesDao messageDao;

        public MessagesLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSN"].ConnectionString;
            this.messageDao = new MessagesDao(connectionString);
        }

        public bool AddMessage(string text, int senderId, int adresseeId)
        {
            if (CheckBadInputString(text))
            {
                return false;
            }

            if (CheckBadId(senderId) || adresseeId < 0)
            {
                return false;
            }

            return messageDao.AddMessage(text, senderId, adresseeId);
        }

        public bool EditMessage(int accountId, int messageId, string accountRole, string text)
        {
            if (CheckBadId(accountId) || CheckBadId(messageId))
            {
                return false;
            }

            if (CheckBadInputString(accountRole))
            {
                return false;
            }

            if (CheckRights(accountId, messageId, accountRole))
            {
                return false;
            }

            return messageDao.EditMessage(messageId, text);
        }

        public IEnumerable<Message> GetAllAccountMessages(int senderAccountId)
        {
            if (CheckBadId(senderAccountId))
            {
                return null;
            }

            return messageDao.GetAllAccountMessages(senderAccountId);
        }

        public IEnumerable<Message> GetAllSharedMessages()
        {
            return messageDao.GetAllSharedMessages();
        }

        public IEnumerable<Message> GetListLastMessages(int quantity, int senderAccountId, int addresseeAccountId)
        {
            return messageDao.GetListLastMessages(quantity, senderAccountId, addresseeAccountId);
        }

        public bool RemoveMessage(int accountId, int messageId, string accountRole)
        {
            if (CheckBadId(accountId) || CheckBadId(messageId))
            {
                return false;
            }

            if (CheckBadInputString(accountRole))
            {
                return false;
            }

            if (CheckRights(accountId, messageId, accountRole))
            {
                return false;
            }

            return messageDao.RemoveMessage(messageId);
        }

        private bool CheckBadInputString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckBadId(int id)
        {
            if (id < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckRights(int accountId, int messageId, string role)
        {
            int senderId = messageDao.GetSenderId(messageId);

            if (accountId != senderId || role != "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
