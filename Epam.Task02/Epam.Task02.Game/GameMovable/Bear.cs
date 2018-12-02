using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class Bear : Monster
    {
        public override void Draw()
        {
            Console.WriteLine("I am bear!");
        }

        public override void Move()
        {
        }

        public override void Shout()
        {
            Console.WriteLine("I`ll crush you!");
        }
    }
}
