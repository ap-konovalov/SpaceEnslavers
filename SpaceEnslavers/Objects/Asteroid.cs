using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEnslavers.Objects
{
    /// <summary>
    /// Объект астероид 
    /// </summary>
    class Asteroid : BaseObject
    {
        public int Power { get; set; }

        public Asteroid(Point pos, Point dir, Size size): base(pos,dir,size)
        {
            Power = 1;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Position.X, Position.Y, Size.Width, Size.Height);
        }
    }
}
