using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Entities
{
    public class ChatList
    {
        public int AccountId { get; set; }

        public IEnumerable<int> ListInterlocutor { get; set; }
    }
}
