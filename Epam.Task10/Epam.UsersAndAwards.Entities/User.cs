using System;

namespace Epam.UsersAndAwards.Entities
{
    public class User
    {
        public int ImageId { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}