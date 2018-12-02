using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class Stone : GameBarrier
    {
        public override void Draw()
        {
            Console.WriteLine($"Stone:{Position.X}, {Position.Y}");
        }
    }
}
