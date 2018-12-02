using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class Wall : GameBarrier
    {
        public override void Draw()
        {
            Console.WriteLine($"Wall:{Position.X}, {Position.Y}");
        }
    }
}
