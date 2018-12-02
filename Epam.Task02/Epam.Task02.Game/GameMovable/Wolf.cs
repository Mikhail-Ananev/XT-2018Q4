using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class Wolf : Monster
    {
        public override void Draw()
        {
            Console.WriteLine("I am wolf!");
        }

        public override void Move()
        {
        }

        public override void Shout()
        {
            Console.WriteLine("I`ll eat you!");
        }
    }
}
