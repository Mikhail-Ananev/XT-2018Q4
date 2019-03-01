using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IChatsLogic
    {
        IEnumerable<ChatList> GetTalkLists(int accountId);
    }
}
