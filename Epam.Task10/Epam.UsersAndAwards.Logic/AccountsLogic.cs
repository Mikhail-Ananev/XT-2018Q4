using Epam.UsersAndAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.Logic
{
    public class AccountsLogic : IAccountsLogic
    {
        public bool CanLogin(string login, string password)
        {
            return login == "Admin" && password == "Odmin";
        }
    }
}
