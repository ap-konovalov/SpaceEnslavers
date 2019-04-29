using SpaceEnslavers.Interfaces;
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
    abstract class BaseObject: ICollision
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

        public bool Collision(ICollision obj) => obj.Rectangle.IntersectsWith(this.Rectangle);
        public Rectangle Rectangle => new Rectangle(Position, Size);
        
        /// <summary>
        /// Отрисовка объекта будет реализована в классах-наследниках, здесь реализации нет, так как метод абстрактный
        /// </summary>
        public abstract void Draw();
   
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
