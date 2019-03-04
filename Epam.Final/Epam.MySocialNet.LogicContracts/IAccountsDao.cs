using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IAccountsDao
    {
        bool SaveNewAccount(Account account);

        bool RemoveAccount(string login);

        bool EditAccount(Account account);

        bool AccountExist(string login);

        bool CheckLogin(string login, byte[] bytePassword);

        bool CheckAdminRole(string login);

        bool SetAdminRole(string login);

        bool RemoveAccountFromAdmins(string login);

        IEnumerable<Account> GetAllAccounts();

        Account GetAccountByLogin(string login);

        Account GetAccountById(int id);

        string GetAccountRole(string login);
    }
}
