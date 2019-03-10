using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.SQLDao
{
    public class ChatsDao : IChatsDao
    {
        private readonly string connectString;

        public ChatsDao(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("Connection string empty!", nameof(connectionString));
            }

            this.connectString = connectionString;
        }

        public IEnumerable<ChatList> GetChatList(int accountId)
        {
            if (accountId < 1)
            {
                return null;
            }

            List<ChatList> chatList = new List<ChatList>();

            using(var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "SELECT FromId, ToId FROM dbo.Messages WHERE (FromId=@id AND ToId>0) OR (ToId=@id)";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = accountId;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int senderId = (int)reader["FromId"];
                    int adresseeId = (int)reader["ToId"];
                    var chat = new ChatList();

                    if (accountId == senderId)
                    {
                        chat.SenderAccountId = senderId;
                        chat.AddresseeAccountId = adresseeId;
                    }
                    else
                    {
                        chat.SenderAccountId = adresseeId;
                        chat.AddresseeAccountId = senderId;
                    }

                    if (chatList.Count == 0)
                    {
                        chatList.Add(chat);
                    }
                    else
                    {
                        foreach (var row in chatList)
                        {
                            if (row.SenderAccountId == chat.SenderAccountId && row.AddresseeAccountId == chat.AddresseeAccountId)
                            {
                                continue;
                            }
                            else
                            {
                                chatList.Add(chat);
                            }
                        }
                    }
                }

                return chatList;
            }
        }
    }
}
