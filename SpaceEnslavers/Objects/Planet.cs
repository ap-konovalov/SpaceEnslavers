using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEnslavers
{
    /// <summary>
    /// отрисовка планеты
    /// </summary>
    class Planet : BaseObject
    {
        // сюда сохраним название планеты, которую хотим нарисовать
        public string _planetName;
        // создадим картинку планеты
        Image Earth = Image.FromFile(@"..\..\planet.png");
        public Planet(Point pos, Point dir, Size size, String PlanetName) : base(pos, dir, size)
        {
           _planetName = PlanetName;
        }

        public override void Draw()
        {
            // проверяем название планеты и рисуем разные планеты
            switch (_planetName)
            {
                case "Earth":
                    //создаем планету
                    Game.Buffer.Graphics.DrawImage(Earth, Position.X, Position.Y, 100, 100);
                    break;
                default:
                    Console.WriteLine("Нет такой планеты");
                    break;
            }
        }

        /// <summary>
        /// траектория движения планеты
        /// </summary>
        public override void Update()
        {
            Position.X = Position.X + Dir.X;

            if (Position.X < 0) Dir.X = -Dir.X;
            if (Position.X > Game.Width) Position.X = -10;

        }
    }
}
