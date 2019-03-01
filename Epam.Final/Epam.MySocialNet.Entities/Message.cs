using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public string Text { get; set; }

        public int SenderAccountId { get; set; }

        public int AddresseeAccountId { get; set; }
    }
}
