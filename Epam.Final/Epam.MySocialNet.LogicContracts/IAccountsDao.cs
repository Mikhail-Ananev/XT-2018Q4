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

        bool RemoveAccount(int id);

        bool EditAccount(Account account);

        bool AccountExist(string login);

        bool CheckLogin(string login, byte[] bytePassword);

        bool SetAdminRole(int id);

        bool RemoveAccountFromAdmins(int id);

        IEnumerable<Account> GetAllAccounts();

        Account GetAccountByLogin(string login);

        Account GetAccountById(int id);

        string GetAccountRole(string login);

        AccountInfo GetAccountInfo(int id);

        bool EditAccountInfo(AccountInfo accountInfo);
    }
}
