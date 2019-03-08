﻿using Epam.MySocialNet.Entities;
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

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Accounts (Login, FirstName, LastName, BirthDay, Password, Role, ImageId) VALUES (@login, @firstName, @lastName, @birthDay, @password, @role, @imageId)";
                cmd.Parameters.AddWithValue("@login", account.Login);
                cmd.Parameters.AddWithValue("@firstName", account.FirstName);
                cmd.Parameters.AddWithValue("@lastName", account.LastName);
                cmd.Parameters.AddWithValue("@birthDay", account.BirthDay);
                cmd.Parameters.Add("@password", SqlDbType.VarBinary, 64).Value = account.Password;
                cmd.Parameters.AddWithValue("@role", account.Role);
                cmd.Parameters.Add("@imageId", SqlDbType.Int).Value = account.ImageId;

                con.Open();

                return cmd.ExecuteNonQuery() == 1;
            }
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

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Accounts SET Login=@login, FirstName=@firstName, LastName=@lastName, Birthday=@birthDay, ImageId=@imageId WHERE Id=@Id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = account.Id });

                cmd.Parameters.AddWithValue("@login", account.Login);
                cmd.Parameters.AddWithValue("@firstName", account.FirstName);
                cmd.Parameters.AddWithValue("@lastName", account.LastName);
                cmd.Parameters.AddWithValue("@birthDay", account.BirthDay);
                cmd.Parameters.Add("@imageId", SqlDbType.Int).Value = account.ImageId;

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
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

    }
}
