using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Entities
{
    public class Friends
    {
        public int SenderId { get; set; }

        public int AdresseeId { get; set; }

        public bool Confirm { get; set; }
    }
}
