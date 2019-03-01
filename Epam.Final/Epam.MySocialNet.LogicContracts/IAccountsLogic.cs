using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IAccountsLogic
    {
        bool SaveNewAccount(string login, string password, int ImageId);

        bool RemoveAccount(string login);

        bool CheckAdminRole(string login);

        bool SetAdminRole(string login);

        bool RemoveAdminRole(string login);

        IEnumerable<Account> GetAllAccounts();

        Account GetAccountById(int id);

        Account GetAccountByLogin(string login);

        bool CheckLogin(string login, string password);

        bool UserExist(string login);

        string GetUserRole(string username);

    }
}
