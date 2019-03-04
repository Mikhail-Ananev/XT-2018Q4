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
                int result = cmd.ExecuteNonQuery();

                return result == 1;
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

        public bool RemoveAccount(string login)
        {
            throw new NotImplementedException();
        }

        public bool EditAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public bool SetAdminRole(string login)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAccountFromAdmins(string login)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAccountRole(string login)
        {
            throw new NotImplementedException();
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
