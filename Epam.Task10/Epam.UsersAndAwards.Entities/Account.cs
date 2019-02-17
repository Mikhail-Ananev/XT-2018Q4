using System;

namespace Epam.UsersAndAwards.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public byte[] Password { get; set; }

        public string Category { get; set; }

        public int ImageId { get; set; }
    }
}