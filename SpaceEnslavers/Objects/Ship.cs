using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SpaceEnslavers.Objects
{
    /// <summary>
    /// Описываем космический корабль
    /// </summary>
    class Ship : BaseObject
    {
        private int _energy = 100;
        public int Energy
        {
            get => _energy;
            set => _energy = value;
        }

        //Статическое событие гибели корабля
        public static event Message MessageDie;
        
        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        public void EnergyRecovery(int health)
        {
            if (Energy <= 90)
            {
                Energy += health;
                Console.WriteLine($"Аптечка. +{health} здоровья");
            }

            if (Energy >= 90 && Energy < 100)
            {
                Energy = 100;
                Console.WriteLine($"Аптечка. {Energy} здоровья");
            }
        }


        public Ship(Point position, Point dir, Size size) : base(position, dir, size)
        {
        }

        /// <summary>
        /// Рисуем корабль
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
        }

        /// <summary>
        /// Движение корабля вверх
        /// </summary>
        public void Up()
        {
            if (Position.Y > 0)
            {
                Position.Y = Position.Y - Dir.Y;
            }
        }

        /// <summary>
        /// Движение корабля вниз
        /// </summary>
        public void Down()
        {
            if (Position.Y < Game.Height)
            {
                Position.Y = Position.Y + Dir.Y;
            }
        }
        public void Die()
        {
            //вызываем событие гибели корабля
            MessageDie?.Invoke();
        }

        public void ShipDamageLog(int damage)
        {
            Console.WriteLine($"Снаряд попал в корабль: -{damage} здоровья. Осталось: {_energy - damage} жизней ");
        }
    }
}