using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Logic
{
    public class AccountsLogic : IAccountsLogic
    {
        public bool CheckAdminRole(string login)
        {
            throw new NotImplementedException();
        }

        public bool CheckLogin(string login, string password)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public string GetUserRole(string username)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAccount(string login)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAdminRole(string login)
        {
            throw new NotImplementedException();
        }

        public bool SaveNewAccount(string login, string password, int ImageId)
        {
            throw new NotImplementedException();
        }

        public bool SetAdminRole(string login)
        {
            throw new NotImplementedException();
        }

        public bool UserExist(string login)
        {
            throw new NotImplementedException();
        }
    }
}
