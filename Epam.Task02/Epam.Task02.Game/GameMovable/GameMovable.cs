﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public abstract class GameMovable : GameObject, IGameMovable
    {
        public abstract void Move();
    }
}
