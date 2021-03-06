﻿using System.Collections.Generic;
using Epam.UsersAndAwards.Entities;


namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IAccountsLogic
    {
        bool SaveNewAccount(string login, string password, int ImageId);

        bool RemoveAccount(string login);

        bool AccountAssignAsAdmin(string login);

        bool RemoveAccountFromAdmins(string login);

        IEnumerable<Account> GetAllAccounts();

        Account GetAccountById(int id);

        Account GetAccountByLogin(string login);

        bool CheckLogin(string login, string password);

        bool IsAccountAdmin(string login);

        bool UserExist(string login);

        string GetUserRole(string username);
    }
}
