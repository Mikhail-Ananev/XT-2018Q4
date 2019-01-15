using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class User
    {
        private string lastName;
        private string middleName;
        private string firstName;
        private int age;

        public string FirstName
        {
            get => this.firstName;
            set
            {
                if (this.Check(value))
                {
                    this.firstName = value;
                }
            }
        }

        public string MiddleName
        {
            get => this.middleName;
            set
            {
                if (this.Check(value))
                {
                    this.middleName = value;
                }
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                if (this.Check(value))
                {
                    this.lastName = value;
                }
            }
        }

        public int Age
        {
            get => this.age;
            set
            {
                if (value < 4 && value > 110)
                {
                    throw new ArgumentException("Incorrect age!", nameof(value));
                }

                this.age = value;
            }
        }

        private bool Check(string str)
        {
            if (str == null)
            {
                throw new ArgumentException("Empty field!", nameof(str));
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                {
                    throw new ArgumentException("The field must contain only letters!", nameof(str));
                }
            }

            if (!char.IsUpper(str[0]))
            {
                throw new ArgumentException("The first symbol must be upper!", nameof(str));
            }

            return true;
        }
    }
}
