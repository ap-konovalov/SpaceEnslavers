using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEnslavers
{
    class Space : BaseObject
    {
        Image background = Image.FromFile(@"..\..\background.jpeg");

        public Space(Point position, Point dir, Size size) : base(position, dir, size)
        {
        }

        public override void Draw()
        {
          Game.Buffer.Graphics.DrawImage(background, Position.X, Position.Y,800,600);
        }
    }
}
