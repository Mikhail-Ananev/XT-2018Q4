using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Round
{
    public abstract class Shape : IVectorGraficsEditor
    {
        public abstract void Show(Point p);
    }
}
