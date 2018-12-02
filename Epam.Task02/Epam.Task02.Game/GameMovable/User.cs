using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class User : GameMovable
    {
        public override void Draw()
        {
            Console.WriteLine("Here I am!");
        }

        public override void Move()
        {
        }

        public int Skill()
        {
            return 1;
        }

        public void Death()
        {
            Console.WriteLine("Game over!");
        }
    }
}
