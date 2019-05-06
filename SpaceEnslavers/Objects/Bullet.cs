using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEnslavers.Objects
{
    /// <summary>
    /// Рисуем снаряд
    /// </summary>
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Рисуем астероид
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Position.X, Position.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Задаем траекторию движения астероида
        /// </summary>
        public override void Update()
        {
            Position.X = Position.X + 53;
        }
    }
}
