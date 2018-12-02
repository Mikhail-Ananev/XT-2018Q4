using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class Strawberry : Bonus
    {
        public override void Draw()
        {
            Console.WriteLine("I love strawberry!");
        }

        public override int SkillBonus()
        {
            return 30;
        }
    }
}
