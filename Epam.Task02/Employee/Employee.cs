using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class Employee : User
    {
        private int experience;
        private string position;

        public int Experience
        {
            get => this.experience;
            set
            {
                if (value > this.Age - 14)
                {
                    throw new ArgumentException("Experience can`t be more than 'Age' minus 14 years!", nameof(value));
                }

                this.experience = value;
            }
        }

        public string Position
        {
            get => this.position;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Work position can`t be empty!", nameof(value));
                }

                this.position = value;
            }
        }
    }
}
