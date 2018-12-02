using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class Tree : GameBarrier
    {
        public override void Draw()
        {
            Console.WriteLine($"Tree:{Position.X}, {Position.Y}");
        }
    }
}
