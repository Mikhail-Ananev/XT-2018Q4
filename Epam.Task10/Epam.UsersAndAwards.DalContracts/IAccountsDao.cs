using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epam.UsersAndAwards.Entities;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IAccountsDao
    {
        bool SaveNewAccount(string login, byte[] password, int imageId);

        bool RemoveAccount(string login);

        bool AccountAssignAsAdmin(string login);

        bool RemoveAccountFromAdmins(string login);

        IEnumerable<Account> GetAllAccounts();

        Account GetAccountById(int id);

        Account GetAccountByLogin(string login);

        bool CheckLogin(string login, byte[] password);

        bool UserExist(string login);

        string GetUserRole(string username);
    }
}
