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
    class Asteroid : BaseObject, ICloneable, Asteroid.IComparable<Asteroid>
    {
        public int Power { get; set; } = 3;
        
        // переменная для подсчета уничтоенных астероидов 
        public static event Message AsteroidDie;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Orange, Position.X, Position.Y, Size.Width, Size.Height);
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

        /// <summary>
        /// Создает клон объекта астероид. Реализуем интерфейс IClonable
        /// </summary>
        /// <returns>объект астероид</returns>
        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(Position.X, Position.Y), new Point(Dir.X, Dir.Y),
                new Size(Size.Width, Size.Height)) {Power = Power};
            return asteroid;
        }

        /// <summary>
        /// Обощенный интерфейс сравнения 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IComparable<T>
        {
            int CompareTo(T obj);
        }
        
        /// <summary>
        /// Реализуем интерфейс IComparable для сравнения двух астероидов
        /// </summary>
        /// <param name="asteroid_2">Астероид</param>
        /// <returns>Больщий астероид</returns>
        int IComparable<Asteroid>.CompareTo(Asteroid asteroid_2)
        {
            if (Power > asteroid_2.Power)
            {
                return 1;
            }
            if (Power < asteroid_2.Power)
            {
                return -1;
            }
            return 0;
        }

        public void Die()
        {
            AsteroidDie?.Invoke();
        }
    }
}