using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    public class Triangle
    {
        private double a;
        private double b;
        private double c;

        public double A
        {
            get => this.a;
            set
            {
                if (this.Test(value))
                {
                    this.a = value;
                }
            }
        }

        public double B
        {
            get => this.b;
            set
            {
                if (this.Test(value))
                {
                    this.b = value;
                }
            }
        }

        public double C
        {
            get => this.c;
            set
            {
                if (this.Test(value))
                {
                    if (this.TestTriangle(this.a, this.b, value))
                    {
                        this.c = value;
                    }
                }
            }
        }

        public double GetTriangleArea
        {
            get
            {
                double p = (this.a + this.b + this.c) / 2;
                return Math.Sqrt(p * (p - this.a) * (p - this.b) * (p - this.c));
            }
        }

        public double GetTrianglePerimeter
        {
            get => this.a + this.b + this.c;
        }

        private bool Test(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Wrong argument: ", nameof(value));
            }

            return true;
        }

        private bool TestTriangle(double a, double b, double c)
        {
            if (a + b > c && a + c > b && b + c > a)
            {
                return true;
            }

            throw new ArgumentException("The triangle is entered incorrectly!");
        }
    }
}
