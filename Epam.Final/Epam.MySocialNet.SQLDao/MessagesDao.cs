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
    public class MessagesDao: IMessagesDao
    {
        private readonly string connectString;

        public MessagesDao(string connectionString)
        {
            this.connectString = connectionString;
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

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Messages (CreationTime, Text, FromId, ToId) VALUES (@creationTime, @text, @fromId, @toId)";
                cmd.Parameters.AddWithValue("@creationTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.Add("@fromId", SqlDbType.Int).Value = senderId;
                cmd.Parameters.Add("@toId", SqlDbType.Int).Value = adresseeId;

                con.Open();

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool EditMessage(int messageId, string text)
        {
            if (CheckBadInputString(text))
            {
                return false;
            }

            if (CheckBadId(messageId))
            {
                return false;
            }

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Messages SET Text=@text WHERE Id=@id";

                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = messageId;

                con.Open();

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public IEnumerable<Message> GetAllAccountMessages(int senderAccountId)
        {
            List<Message> messages = new List<Message>();

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, CreationTime, Text, ToId FROM dbo.Messages WHERE FromId = @id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = senderAccountId;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    DateTime creationTime = (DateTime)reader["CreationTime"];
                    string text = (string)reader["Text"];
                    int adresseeAccountId = (int)reader["ToId"];

                    Message message = new Message()
                    {
                        Id = id,
                        CreationTime = creationTime,
                        Text = text,
                        SenderAccountId = senderAccountId,
                        AddresseeAccountId = adresseeAccountId,
                    };

                    messages.Add(message);
                }
            }

            return messages;
        }

        public IEnumerable<Message> GetAllSharedMessages()
        {
            int adresseeId = 0;
            List<Message> messages = new List<Message>();

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, CreationTime, Text, FromId FROM dbo.Messages WHERE ToId = @id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = adresseeId;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    DateTime creationTime = (DateTime)reader["CreationTime"];
                    string text = (string)reader["Text"];
                    int senderId = (int)reader["FromId"];

                    Message message = new Message()
                    {
                        Id = id,
                        CreationTime = creationTime,
                        Text = text,
                        SenderAccountId = senderId,
                        AddresseeAccountId = adresseeId,
                    };

                    messages.Add(message);
                }
            }

            return messages;
        }

        public IEnumerable<Message> GetListLastMessages(int quantity, int senderAccountId, int addresseeAccountId)
        {
            if (CheckBadId(senderAccountId) || addresseeAccountId < 0 || quantity < 1)
            {
                return null;
            }

            List<Message> messages = new List<Message>();

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, CreationTime, Text FROM dbo.Messages WHERE FromId = @fromId AND ToId = @toId";
                cmd.Parameters.Add("@fromId", SqlDbType.Int).Value = senderAccountId;
                cmd.Parameters.Add("@toId", SqlDbType.Int).Value = addresseeAccountId;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() && quantity > 0)
                {
                    int id = (int)reader["Id"];
                    DateTime creationTime = (DateTime)reader["CreationTime"];
                    string text = (string)reader["Text"];

                    Message message = new Message()
                    {
                        Id = id,
                        CreationTime = creationTime,
                        Text = text,
                        SenderAccountId = senderAccountId,
                        AddresseeAccountId = addresseeAccountId,
                    };

                    messages.Add(message);
                    quantity--;
                }
            }

            return messages;
        }

        public int GetSenderId(int messageId)
        {
            if (CheckBadId(messageId))
            {
                return -1;
            }

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT CreationTime, FromId, ToId FROM dbo.Messages WHERE Id = @id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = messageId;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return (int)reader["FromId"];
                }
                else
                {
                    return -1;
                }
            }
        }

        public bool RemoveMessage(int messageId)
        {
            if (CheckBadId(messageId))
            {
                return false;
            }

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.Messages WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", messageId);

                return cmd.ExecuteNonQuery() == 1;
            }
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
    }
}
