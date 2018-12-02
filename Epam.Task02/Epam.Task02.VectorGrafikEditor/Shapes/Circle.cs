using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Round
{
    public class Circle : Shape
    {
        public override void Show(Point p)
        {
            Console.WriteLine($"Drawing \"Circle\" located at the coordinates:{Environment.NewLine}" +
                $"X:{p.X}{Environment.NewLine}" +
                $"Y:{p.Y}{Environment.NewLine}");
        }
    }
}
