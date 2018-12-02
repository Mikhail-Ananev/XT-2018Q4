using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class Apple : Bonus
    {
        public override void Draw()
        {
            Console.WriteLine("Nice apple!");
        }

        public override int SkillBonus()
        {
            return 10;
        }
    }
}
