using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEnslavers
{
    class Star : BaseObject
    {
        public Star(Point pos,Point dir, Size size): base(pos,dir,size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Position.X + Size.Width, Position.Y, Position.X, Position.Y + Size.Height);
        }

        public override void Update()
        {
            //    Position.X = Position.X - Dir.X;
            //    if (Position.X < 0) Position.X = Game.Width - Size.Width;

            Position.X = Position.X + Dir.X;

            if (Position.X < 0) Dir.X = -Dir.X;
            if (Position.X > Game.Width) Dir.X = -Dir.X;
        
        }
    }
}
