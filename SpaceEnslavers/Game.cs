using SpaceEnslavers.Objects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceEnslavers
{
    internal class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        // Game space parameters
        public static int Width { get; set; }
        public static int Height { get; set; }

        //переменная для снаряда
        private static Bullet _bullet;
        //массивчик с астероидами
        private static Asteroid[] _asteroids;

        static Game()
        { 
        }

        public static BaseObject[] _objs;

        public static void Load()
        {

            _objs = new BaseObject[30];

            //нарисуем фон
            _objs[0] = new Space(new Point(0, 0), new Point(0, 0), new Size(5, 5));

            //нарисуем планету
            _objs[1] = new Planet(new Point(20, 20), new Point(10, 10), new Size(5, 5), "Earth");

            //нарисуем снаряд
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));

            //Создадим астероиды
            _asteroids = new Asteroid[3];

            var rnd = new Random();

            // нарисуем летающие звезды 
            for (int i = 2; i < _objs.Length; i++)
            {
                int random = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(20, rnd.Next(0, Game.Height)), new Point(-random, random), new Size(3, 3));
            }

            for (var i = 0; i < _asteroids.Length; i++)
            {
                int random = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(100, rnd.Next(0, Game.Height)), new Point(-random / 5, random), new Size(random, random));
            }

        }
        internal static void Init(Form form)
        {
            Graphics g;

            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();


            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            // отрисовали фон звезды и планету
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }

            // вызвали метод отрисовки для снаряда
            _bullet.Draw();

            //отрисовали все астероиды
            foreach (Asteroid item in _asteroids)
            {
                item.Draw();
            }

            Buffer.Render();
        }

        public static void Update()
        {
            // обновляем позицию фона звезд и планеты
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
            }

            // обновляем позицию астероидов
            foreach (Asteroid item in _asteroids)
            {
                item.Update();
            }

            //Обновляем позицию снаряда
            _bullet.Update();

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}