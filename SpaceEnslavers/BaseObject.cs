using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEnslavers
{
    /// <summary>
    /// Базовый обект, от которого будем наследовать остальные
    /// </summary>
    class BaseObject
    {
        protected Point Position;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Конструктор  базового объекта
        /// </summary>
        /// <param name="position"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public BaseObject(Point position, Point dir, Size size)
        {
            Position = position;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Отрисовка базового объекта
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Position.X, Position.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Движение обекта
        /// </summary>
        public virtual void Update()
        {
            Position.X = Position.X + Dir.X;
            Position.Y = Position.Y + Dir.Y;
            if (Position.X < 0) Dir.X = -Dir.X;
            if (Position.X > Game.Width) Dir.X = -Dir.X;
            if (Position.Y < 0) Dir.Y = -Dir.Y;
            if (Position.Y > Game.Height) Dir.Y = -Dir.Y;
        }

    }
}
