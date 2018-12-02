using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task02.Game
{
    public abstract class GameObject : IGameObject
    {
        public Point Position
        {
            get => this.Position;
            set
            {
                if (value.X > Program.MapWidth && value.X < 0)
                {
                    throw new ArgumentException("Incorrect coordinate:", nameof(value.X));
                }
                else
                {
                    this.Position.X = value.X;
                }

                if (value.Y > Program.MapHeight && value.Y < 0)
                {
                    throw new ArgumentException("Incorrect coordinate:", nameof(value.Y));
                }
                else
                {
                    this.Position.Y = value.Y;
                }
            }
        }

        public abstract void Draw();
    }
}
