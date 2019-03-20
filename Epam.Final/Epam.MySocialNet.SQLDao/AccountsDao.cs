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
    public class AccountsDao : IAccountsDao
    {
        public readonly string connectString;

        public AccountsDao(string connectionString)
        {
            if (CheckBadInputString(connectionString))
            {
                throw new ArgumentNullException("Connection string empty!", nameof(connectionString));
            }

            this.connectString = connectionString;
        }

        public bool SaveNewAccount(Account account)
        {
            if (account == null)
            {
                return false;
            }

            if (AccountExist(account.Login))
            {
                return false;
            }

            int id = 0;
            bool result = false;

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Accounts (Login, FirstName, LastName, BirthDay, Password, Role, ImageId)" +
                    " VALUES (@login, @firstName, @lastName, @birthDay, @password, @role, @imageId);SELECT scope_identity()";
                cmd.Parameters.AddWithValue("@login", account.Login);
                cmd.Parameters.AddWithValue("@firstName", account.FirstName);
                cmd.Parameters.AddWithValue("@lastName", account.LastName);
                cmd.Parameters.AddWithValue("@birthDay", account.BirthDay);
                cmd.Parameters.Add("@password", SqlDbType.VarBinary, 64).Value = account.Password;
                cmd.Parameters.AddWithValue("@role", account.Role);
                cmd.Parameters.Add("@imageId", SqlDbType.Int).Value = account.ImageId;

                con.Open();
                id = (int)(decimal)cmd.ExecuteScalar();

                result = cmd.ExecuteNonQuery() == 1;
            }

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.AccountInfo AccountId=@id, Sex=@sex, City=@city, Language=@language, Family=@family, Education=@education) Values (@id)";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });
                cmd.Parameters.AddWithValue("@sex", null);
                cmd.Parameters.AddWithValue("@city", null);
                cmd.Parameters.AddWithValue("@language", null);
                cmd.Parameters.AddWithValue("@family", null);
                cmd.Parameters.AddWithValue("@education", null);

                connect.Open();
            }

            return result;
        }

        public bool AccountExist(string login)
        {
            if (CheckBadInputString(login))
            {
                return false;
            }

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, Login, Password, Role FROM dbo.Accounts WHERE Login=@login"; //Возможно убрать любую выборку ибо не используется
                cmd.Parameters.AddWithValue("@login", login);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckLogin(string login, byte[] password)
        {
            if (CheckBadInputString(login))
            {
                return false;
            }

            if (password == null)
            {
                return false;
            }

            byte[] readpassword;
            readpassword = null;

            //var shaM = new SHA512Managed();

            //var stringBytes = Encoding.UTF8.GetBytes("Odmin");

            //var bytePassword = shaM.ComputeHash(stringBytes);

            //using (SqlConnection connect = new SqlConnection(connectString))
            //{
            //    SqlCommand cmd = connect.CreateCommand();
            //    cmd.CommandText = "UPDATE dbo.Accounts SET Password=@bytePassword WHERE Id=@Id";
            //    cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = 5 });

            //    cmd.Parameters.AddWithValue("@bytePassword", bytePassword);

            //    connect.Open();
            //    int result = cmd.ExecuteNonQuery();
            //}

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Password FROM dbo.Accounts WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", login);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    readpassword = (byte[])reader["Password"];
                }
                else
                {
                    return false;
                }

            }

            return (password.SequenceEqual(readpassword));
        }

        public bool CheckAdminRole(string login)
        {
            if (CheckBadInputString(login))
            {
                return false;
            }

            string role = null;
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Role FROM dbo.Accounts WHERE Login=@login";

                cmd.Parameters.AddWithValue("@login", login);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    role = (string)reader["Role"];
                }
                else
                {
                    return false;
                }
            }

            return role == "Admin";
        }



        public bool RemoveAccount(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.Accounts WHERE Id=@Id; DELETE FROM dbo.Chats WHERE WHERE SenderAccountId=@id OR AddresseeAccountId=@id; " +
                                  "DELETE FROM dbo.Messages WHERE SenderAccountId=@id OR AddresseeAccountId=@id";
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EditAccount(Account account)
        {
            if (account == null)
            {
                return false;
            }

            string commandParameters = GetUpdateData(account);

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Accounts SET"+ commandParameters + "WHERE Id=@Id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = account.Id });
                if (commandParameters.Contains("@login"))
                {
                    cmd.Parameters.AddWithValue("@login", account.Login);
                }
                if (commandParameters.Contains("@firstName"))
                {
                    cmd.Parameters.AddWithValue("@firstName", account.FirstName);
                }
                if (commandParameters.Contains("@lastName"))
                {
                    cmd.Parameters.AddWithValue("@lastName", account.LastName);
                }
                if (commandParameters.Contains("@birthDay"))
                {
                    cmd.Parameters.AddWithValue("@birthDay", account.BirthDay);
                }
                if (commandParameters.Contains("@password"))
                {
                    cmd.Parameters.Add("@password", SqlDbType.VarBinary, 64).Value = account.Password;
                }

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        private string GetUpdateData(Account account)
        {
            var sb = new StringBuilder();

            if (account.Login != null)
            {
                sb.Append(", Login=@login");
            }

            if (account.Password != null)
            {
                sb.Append(", Password=@password");
            }

            if (account.FirstName != null)
            {
                sb.Append(", FirstName=@firstName");
            }

            if (account.LastName != null)
            {
                sb.Append(", LastName=@lastName");
            }

            if (account.BirthDay != DateTime.MinValue)
            {
                sb.Append(", BirthDay=@birthDay");
            }

            if (sb.Length > 0)
            {
                sb.Remove(0, 1).Append(' ');
            }

            return sb.ToString();
        }

        public bool SetAdminRole(int id)
        {
            string role = "Admin";

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Accounts SET Role=@role WHERE Id=@Id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = id });
                cmd.Parameters.AddWithValue("@role", role);

                con.Open();

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool RemoveAccountFromAdmins(int id)
        {
            string role = "User";

            bool adminIsAlone = true;

            foreach (var account in GetAllAccounts())
            {
                if ((account.Role == "Admin") && (account.Id != id))
                {
                    adminIsAlone = false;
                    break;
                }
            }

            if (adminIsAlone)
            {
                return false;
            }

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Accounts SET Role=@role WHERE Id=@id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = id });
                cmd.Parameters.AddWithValue("@role", role);

                con.Open();

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            List<Account> allAccounts = new List<Account>();

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, Login, FirstName, LastName, BirthDay, ImageId FROM dbo.Accounts";

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string login = (string)reader["Login"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    DateTime birthDay = (DateTime)reader["BirthDay"];
                    int imageId = (int)reader["ImageId"];

                    Account account = new Account()
                    {
                        Id = id,
                        Login = login,
                        FirstName = firstName,
                        LastName = lastName,
                        BirthDay = birthDay,
                        ImageId = imageId,
                    };

                    allAccounts.Add(account);
                }
            }

            return allAccounts;
        }

        public Account GetAccountByLogin(string login)
        {
            if (CheckBadInputString(login))
            {
                return null;
            }

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT Id, FirstName, LastName, BirthDay, ImageId FROM dbo.Accounts WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", login);

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                return new Account
                {
                    Id = (int)reader["Id"],
                    Login = login,
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    BirthDay = (DateTime)reader["BirthDay"],
                    ImageId = (int)reader["ImageId"],
                };
            }
        }

        public Account GetAccountById(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT Login, FirstName, LastName, BirthDay, ImageId FROM dbo.Accounts WHERE id=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                return new Account
                {
                    Id = id,
                    Login = (string)reader["Login"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    BirthDay = (DateTime)reader["BirthDay"],
                    ImageId = (int)reader["ImageId"],
                };
            }
        }

        public string GetAccountRole(string login)
        {
            if (CheckBadInputString(login))
            {
                return null;
            }

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT Role FROM dbo.Accounts WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", login);

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                return (string)reader["Role"];
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

        public AccountInfo GetAccountInfo(int id)
        {
            AccountInfo accountInfo = null;

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT Sex, City, Language, Family, Education FROM dbo.AccountInfo WHERE AccountId=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    accountInfo = new AccountInfo
                    {
                        AccountId = id,
                        Sex = reader["Sex"] as string,
                        City = reader["City"] as string,
                        Language = reader["Language"] as string,
                        Family = reader["Family"] as string,
                        Education = reader["Education"] as string,
                    };
                }
            }

            if (accountInfo == null)
            {
                using(SqlConnection connect = new SqlConnection(connectString))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    cmd.CommandText = "INSERT INTO dbo.AccountInfo Values (@id, @sex, @city, @language, @family, @education)";
                    cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });
                    cmd.Parameters.AddWithValue("@sex", DBNull.Value);
                    cmd.Parameters.AddWithValue("@city", DBNull.Value);
                    cmd.Parameters.AddWithValue("@language", DBNull.Value);
                    cmd.Parameters.AddWithValue("@family", DBNull.Value);
                    cmd.Parameters.AddWithValue("@education", DBNull.Value);

                    connect.Open();

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("Данные не были записаны!");
                    }
                }
            }

            return accountInfo;
        }

        public bool EditAccountInfo(AccountInfo accountInfo)
        {
            if (accountInfo == null)
            {
                return false;
            }

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.AccountInfo SET Sex=@sex, City=@city, Language=@language, Family=@family, Education=@education WHERE AccountId=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = accountInfo.AccountId });

                cmd.Parameters.AddWithValue("@sex", accountInfo.Sex);
                cmd.Parameters.AddWithValue("@city", accountInfo.City);
                cmd.Parameters.AddWithValue("@language", accountInfo.Language);
                cmd.Parameters.AddWithValue("@family", accountInfo.Family);
                cmd.Parameters.AddWithValue("@education", accountInfo.Education);

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public void UpdateImageId(int accountId, int imageId)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Accounts SET ImageId=@imageId WHERE Id=@accountId";
                cmd.Parameters.Add(new SqlParameter("@accountId", DbType.Int32) { Value = accountId });
                cmd.Parameters.Add(new SqlParameter("@imageId", DbType.Int32) { Value = imageId });

                connect.Open();
                int result = cmd.ExecuteNonQuery();
            }
        }
    }
}
