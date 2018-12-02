using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public class RottenTangerin : Bonus
    {
        public override void Draw()
        {
            Console.WriteLine("Oh shit...");
        }

        public override int SkillBonus()
        {
            return -5;
        }
    }
}
