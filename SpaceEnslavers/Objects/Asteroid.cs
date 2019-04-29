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

        public override void Update()
        {
            Position.X = Position.X + Dir.X;
            Position.Y = Position.Y + Dir.Y;
            if (Position.X < 0) Dir.X = -Dir.X;
            if (Position.X > Game.Width) Dir.X = -Dir.X;
            if (Position.Y < 0) Dir.Y = -Dir.Y;
            if (Position.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        public void Regeneration()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Position.X, Position.Y, Size.Width, Size.Height);
        }
          
    }
}
