using System.Drawing;
using System.Threading;

namespace SpaceEnslavers.Objects
{ 
    /// <summary>
    /// Аптечка. Будет лечить кораболь
    /// </summary>
    class FirstAidKit: BaseObject
    {
        private int _health = 10;
        public int Health => _health;
        
        public FirstAidKit(Point position, Point dir, Size size) : base(position, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Brown, Position.X, Position.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.FillRectangle(Brushes.White, Position.X + 3, Position.Y + 7, Size.Width-5, Size.Height/4 );
            Game.Buffer.Graphics.FillRectangle(Brushes.White, Position.X + 7, Position.Y + 3, Size.Width/4, Size.Height-5 );

        }

        public override void Update()
        {
            Position.X = Position.X - Dir.X *5;
            if (Position.X < 0) Dir.X = -Dir.X;
            if (Position.X > Game.Width)
            {
                Dir.X = -Dir.X;
            }
        }
    }
}