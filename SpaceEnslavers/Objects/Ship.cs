using System.Drawing;

namespace SpaceEnslavers.Objects
{
    /// <summary>
    /// Описываем космический корабль
    /// </summary>
    class Ship : BaseObject
    {
        private int _energy = 100;
        public int Energy => _energy;

        //Статическое событие гибели корабля
        public static event Message MessageDie;

        public void EnergyLow(int n)
        {
            _energy -= n;
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
            //вызываем событие гибели корабля, т.е. делегат MessageDie
            MessageDie?.Invoke();
        }
    }
}