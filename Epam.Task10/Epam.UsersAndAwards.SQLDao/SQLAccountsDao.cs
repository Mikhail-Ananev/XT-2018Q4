using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Epam.UsersAndAwards.DalContracts;
using System.Security.Cryptography;
using System.Text;

namespace Epam.UsersAndAwards.SQLDao
{
    public class SQLAccountsDao : IAccountsDao
    {
        public readonly string connectString;

        public SQLAccountsDao(string connectionString)
        {
            this.connectString = connectionString;
        }

        public bool SaveNewAccount(string login, byte[] password, int imageId)
        {
            string category = "User";

            if (UserExist(login))
            {
                return false;
            }

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Accounts (Login, Password, Category, ImageId) VALUES (@login, @password, @category, @imageId)";
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.Add("@Password", SqlDbType.VarBinary, 64).Value = password;
                cmd.Parameters.Add(new SqlParameter("@category", DbType.String) { Value = category });
                cmd.Parameters.Add(new SqlParameter("@imageId", DbType.Int32) { Value = imageId });

                con.Open();
                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool UserExist(string login)
        {
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, Login, Password, Category FROM dbo.Accounts WHERE Login=@login";
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

        public bool RemoveAccount(string login)
        {
            bool adminIsAlone = true;

            foreach (var account in GetAllAccounts())
            {
                if ((account.Category == "Admin") && (account.Login != login))
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
                cmd.CommandText = "DELETE FROM dbo.Accounts WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", login);

                con.Open();
                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool AccountAssignAsAdmin(string login)
        {
            string category = "Admin";

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Accounts SET Category=@category WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.Add(new SqlParameter("@category", DbType.String) { Value = category });

                con.Open();
                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool RemoveAccountFromAdmins(string login)
        {
            string category = "User";

            bool adminIsAlone = true;

            foreach (var account in GetAllAccounts())
            {
                if ((account.Category == "Admin") && (account.Login != login))
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
                cmd.CommandText = "UPDATE dbo.Accounts SET Category=@category WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.Add(new SqlParameter("@category", DbType.String) { Value = category });

                con.Open();
                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            List<Account> allAccounts = new List<Account>();

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, Login, Password, Category FROM dbo.Accounts";

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string login = (string)reader["Login"];
                    byte[] password = (byte[])reader["Password"];
                    string category = (string)reader["Category"];

                    Account account = new Account()
                    {
                        Id = id,
                        Login = login,
                        Password = password,
                        Category = category
                    };

                    allAccounts.Add(account);
                }
            }

            return allAccounts;
        }

        public Account GetAccountById(int id)
        {
            Account account = new Account();
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Login, Password, Category FROM dbo.Accounts WHERE Id=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                string login = (string)reader["Login"];
                byte[] password = (byte[])reader["Password"];
                string category = (string)reader["Category"];

                account.Id = id;
                account.Login = login;
                account.Password = password;
                account.Category = category;
            }
            return account;
        }

        public Account GetAccountByLogin(string login)
        {
            Account account = new Account();
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, Password, Category, ImageId FROM dbo.Accounts WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", login);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                int id = (int)reader["Id"];
                byte[] password = (byte[])reader["Password"];
                string category = (string)reader["Category"];
                int imageId = (int)reader["ImageId"];

                account.Id = id;
                account.Login = login;
                account.Password = password;
                account.Category = category;
                account.ImageId = imageId;
            }
            return account;
        }

        public bool CheckLogin(string login, byte[] password)
        {
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

        public string GetUserRole(string username)
        {
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Category FROM dbo.Accounts WHERE Login=@login";
                cmd.Parameters.AddWithValue("@login", username);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                string role = null;

                if (reader.Read())
                {
                    role = (string)reader["Category"];
                }

                return role;

            }
        }
    }
}
