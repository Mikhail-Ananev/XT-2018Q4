using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Round
{
    public class Round
    {
        private Point center = new Point();
        private double radius;

        public double Radius
        {
            get
            {
                return this.radius;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Radius must be more than zero", nameof(value));
                }
                else
                {
                    this.radius = value;
                }
            }
        }

        public double GetLength
        {
            get
            {
                return 2 * this.radius * Math.PI;
            }
        }

        public double GetArea
        {
            get
            {
                return this.radius * this.radius * Math.PI;
            }
        }

        internal Point Center { get => this.center; set => this.center = value; }
    }
}
