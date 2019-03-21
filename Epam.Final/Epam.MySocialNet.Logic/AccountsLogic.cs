using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;
using Epam.MySocialNet.SQLDao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Logic
{
    public class AccountsLogic : IAccountsLogic
    {
        private IAccountsDao accountDao;

        private SHA512 shaM = new SHA512Managed();

        public AccountsLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSN"].ConnectionString;
            this.accountDao = new AccountsDao(connectionString);
        }


        public bool CheckAdminRole(string login)
        {
            if (CheckBadInputString(login))
            {
                return false;
            }

            if (accountDao.GetAccountRole(login) == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckLogin(string login, string password)
        {
            if (CheckBadInputString(login) || CheckBadInputString(password))
            {
                return false;
            }

            var stringBytes = Encoding.UTF8.GetBytes(password);

            var bytePassword = shaM.ComputeHash(stringBytes);

            return accountDao.CheckLogin(login, bytePassword);
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
            if (CheckBadInputString(login))
            {
                return null;
            }

            return accountDao.GetAccountByLogin(login);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return accountDao.GetAllAccounts().ToList();
        }

        public string GetAccountRole(string login)
        {
            return accountDao.GetAccountRole(login);
        }

        public bool RemoveAccount(int id)
        {
            return accountDao.RemoveAccount(id);
        }

        public bool RemoveAdminRole(int id)
        {
            return accountDao.RemoveAccountFromAdmins(id);
        }

        public bool SaveNewAccount(string login, string firstName, string lastName, string password, string role, string birthDayStr, int imgId)
        {

            if (CheckBadInputString(login) || CheckBadInputString(password))
            {
                return false;
            }

            if (CheckBadInputString(login))
            {
                return false;
            }

            if (accountDao.AccountExist(login))
            {
                return false;
            }

            if (!CheckAccountData(login, firstName, lastName, birthDayStr))
            {
                return false;
            }

            var stringBytes = Encoding.UTF8.GetBytes(password);
            var bytePassword = shaM.ComputeHash(stringBytes);

            if (!this.CheckRole(role))
            {
                return false;
            }

            var account = new Account
            {
                Login = login,
                FirstName = firstName,
                LastName = lastName,
                Password = bytePassword,
                Role = role,
                ImageId = imgId,
                BirthDay = DateTime.Parse(birthDayStr),
            };

            return accountDao.SaveNewAccount(account);
        }

        public int GetAge(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (birthday > now.AddYears(-age)) age--;

            return age;
        }

        private bool CheckName(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("Имя или фамилия должны содержать символы!");
                return false;
            }

            if (!char.IsUpper(str[0]))
            {
                Console.WriteLine("Первый символ имени или фамилии должен быть заглавным!");
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                {
                    Console.WriteLine("Имя или фамилия должны состоять только из букв!");
                    return false;
                }
            }

            return true;
        }

        private bool CheckLogin(string login)
        {
            var regex = new Regex(@"\b[A-Za-z0-9]([A-Za-z0-9.\-_]*[A-Za-z0-9])*@([A-Za-z0-9]([A-Za-z0-9\-]*[A-Za-z0-9])*\.)+[A-Za-z]{2,6}\b");
            if (regex.IsMatch(login))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckRole(string role)
        {
            if (role == "Admin" || role == "User")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetAdminRole(int id)
        {
            return accountDao.SetAdminRole(id);
        }

        public bool AccountExist(string login)
        {
            if (CheckBadInputString(login))
            {
                return false;
            }

            return accountDao.AccountExist(login);
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

        public bool EditAccount(string strId, string newLogin, string newFirstName, string newLastName, string newStrBirthDay, string newPassword, string oldPassword)
        {
            if (CheckBadInputString(strId) || CheckBadInputString(newLogin) || CheckBadInputString(newFirstName) || CheckBadInputString(newLastName) || CheckBadInputString(newStrBirthDay))
            {
                return false;
            }

            if (!CheckAccountData(newLogin, newFirstName, newLastName, newStrBirthDay))
            {
                return false;
            }

            var newBirthDay = DateTime.Parse(newStrBirthDay);

            int id;
            bool result = int.TryParse(strId, out id);
            if (!result)
            {
                return false;
            }

            var oldAccountData = accountDao.GetAccountById(id);

            var newAccountData = new Account
            {
                Id = id,
                Login = newLogin,
                FirstName = newFirstName,
                LastName = newLastName,
                BirthDay = newBirthDay,
            };

            if (!string.IsNullOrEmpty(oldPassword))
            {
                var stringBytes = Encoding.UTF8.GetBytes(oldPassword);
                var byteOldPassword = shaM.ComputeHash(stringBytes);

                if (accountDao.CheckLogin(oldAccountData.Login, byteOldPassword))
                {
                    stringBytes = Encoding.UTF8.GetBytes(newPassword);
                    newAccountData.Password = shaM.ComputeHash(stringBytes);
                }
                else
                {
                    return false;
                }
            }


            GetUpdatedData(oldAccountData, newAccountData);

            return accountDao.EditAccount(newAccountData);
        }

        private static void GetUpdatedData(Account oldAccountData, Account newAccountData)
        {
            if (newAccountData.Login == oldAccountData.Login)
            {
                newAccountData.Login = null;
            }

            if (newAccountData.FirstName == oldAccountData.FirstName)
            {
                newAccountData.FirstName = null;
            }

            if (newAccountData.LastName == oldAccountData.LastName)
            {
                newAccountData.LastName = null;
            }

            if (newAccountData.BirthDay.ToShortDateString() == oldAccountData.BirthDay.ToShortDateString())
            {
                newAccountData.BirthDay = DateTime.MinValue;
            }
        }

        public AccountInfo GetAccountInfo(int id)
        {
            return accountDao.GetAccountInfo(id);
        }

        public bool EditAccountInfo(AccountInfo accountInfo)
        {
            return accountDao.EditAccountInfo(accountInfo);
        }

        public void UpdateImageId(int accountId, int imageId)
        {
            accountDao.UpdateImageId(accountId, imageId);
        }

        private bool CheckAccountData(string login, string firstName, string lastName, string birthDayStr)
        {
            if (!this.CheckName(firstName))
            {
                return false;
            }

            if (!this.CheckName(lastName))
            {
                return false;
            }

            DateTime birthDay;
            bool result = DateTime.TryParse(birthDayStr, out birthDay);
            if (!result)
            {
                return false;
            }

            int age = GetAge(birthDay);
            if (age < 4 || age > 125)
            {
                return false;
            }

            return true;
        }
    }
}
