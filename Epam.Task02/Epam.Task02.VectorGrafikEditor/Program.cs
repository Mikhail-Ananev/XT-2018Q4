using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Round
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            var coordinate = new Random();
            Point center = new Point();
            IVectorGraficsEditor[] shape =
            {
                new Line(),
                new Circle(),
                new Rectangle(),
                new Ring(),
                new NewRound(),
            };
            for (int i = 0; i < shape.Length; i++)
            {
                center.X = coordinate.Next(-999, 999);
                center.Y = coordinate.Next(-999, 999);
                shape[i].Show(center);
            }
        }
    }
}
