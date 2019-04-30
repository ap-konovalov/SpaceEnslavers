using SpaceEnslavers.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceEnslavers
{
    /// <summary>
    /// Базовый обект, от которого будем наследовать остальные
    /// </summary>
    abstract class BaseObject : ICollision
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

            //Проверка что размер объекта не отрицательное число
            if (size.Width < 0 || size.Height < 0)
            {
                throw new GameObjectException(
                    "При создании объекта произошла ошибка.\n Размер объекта должен быть положительным числом");
            }

            // Проверка что скорость объекта не превышает 100
            if (Dir.X > 100 || Dir.Y > 100)
            {
                throw new GameObjectException(
                    "При создании объекта произошла ошибка.\n скорость объекта должен быть не больше 100");
            }
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
        public abstract void Update();
    }

    /// <summary>
    /// Собственное исключение при попытке  создать объект с : отрицательным размером, слишком большой скорость или неверной позицией)
    /// </summary>
    class GameObjectException : Exception
    {
        public GameObjectException(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}