using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Round
{
    public class Ring
    {
        private Point center = new Point();
        private Round innerRound = new Round();
        private Round outerRound = new Round();

        public Ring(double outer, double inner)
        {
            if (outer < inner)
            {
                throw new ArgumentException("Incorrect inner and outer radius value");
            }

            this.innerRound.Radius = inner;
            this.outerRound.Radius = outer;
        }

        public double InnerRadius
        {
            get => this.innerRound.Radius;
            set
            {
                if (value >= this.outerRound.Radius)
                {
                    throw new ArgumentException("Inner radius must be smaller than outer radius", nameof(this.innerRound.Radius));
                }

                this.innerRound.Radius = value;
            }
        }

        public double OuterRadius
        {
            get => this.outerRound.Radius;
            set
            {
                if (value <= this.innerRound.Radius)
                {
                    throw new ArgumentException("Outer radius must be bigger than inner radius", nameof(this.outerRound.Radius));
                }

                this.innerRound.Radius = value;
            }
        }

        public double RingArea
        {
            get => this.outerRound.GetArea - this.innerRound.GetArea;
        }

        public double RingLength
        {
            get => this.outerRound.GetLength + this.innerRound.GetLength;
        }

        internal Point Center { get => this.center; set => this.center = value; }
    }
}
