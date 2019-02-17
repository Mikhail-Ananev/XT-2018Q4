using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.DalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Epam.UsersAndAwards.SQLDao;
using System.Configuration;

namespace Epam.UsersAndAwards.Logic
{
    public class AccountsLogic : IAccountsLogic
    {
        private IAccountsDao accountDao;

        private SHA512 shaM = new SHA512Managed();

        public AccountsLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UaADB"].ConnectionString;
            this.accountDao = new SQLAccountsDao(connectionString);
        }

        public bool SaveNewAccount(string login, string password, int imageId)
        {

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var stringBytes = Encoding.UTF8.GetBytes(password);

            var bytePassword = shaM.ComputeHash(stringBytes);

            return accountDao.SaveNewAccount(login, bytePassword, imageId);
        }

        public bool RemoveAccount(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }

            return accountDao.RemoveAccount(login);
        }

        public bool AccountAssignAsAdmin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }

            return accountDao.AccountAssignAsAdmin(login);
        }

        public bool RemoveAccountFromAdmins(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }

            return accountDao.RemoveAccountFromAdmins(login);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return accountDao.GetAllAccounts().ToArray();
        }

        public Account GetAccountById(int id)
        {
            if (id < 1)
            {
                return null;
            }
            return accountDao.GetAccountById(id);
        }

        public Account GetAccountByLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return null;
            }
            return accountDao.GetAccountByLogin(login);
        }

        public bool CheckLogin(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var stringBytes = Encoding.UTF8.GetBytes(password);

            var bytePassword = shaM.ComputeHash(stringBytes);

            return accountDao.CheckLogin(login, bytePassword);
        }

        public bool IsAccountAdmin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }

            Account account = GetAccountByLogin(login);
            if (account == null)
            {
                return false;
            }
            return GetAccountByLogin(login).Category == "Admin";
        }

        public bool UserExist(string login)
        {
            return accountDao.UserExist(login);
        }

        public string GetUserRole(string username)
        {
            return accountDao.GetUserRole(username);
        }
    }
}
