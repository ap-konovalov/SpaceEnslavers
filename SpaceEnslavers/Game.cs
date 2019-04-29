using SpaceEnslavers.Objects;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
            var rnd = new Random();
            // обновляем позицию фона звезд и планеты
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
            }
            
            //нужно чтобы понимать с каким астероидом столкнулся снаряд и перересовать его
            int currentAsteroid = 0;
            // обновляем позицию астероидов
            foreach (Asteroid asteroid in _asteroids)
            {
                asteroid.Update();
             
                //если астероид столкнулся с выстрелом воспроизводим звук
                if (asteroid.Collision(_bullet))
                {
                    SystemSounds.Hand.Play();
                    //вместо столкнувшегося астероида рисуем новый
                    int random = rnd.Next(5, 50);
                    _asteroids[currentAsteroid] = new Asteroid(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)), new Point(-random / 5, random), new Size(random, random));
                }
                currentAsteroid++;
            }

            //Обновляем позицию снаряда
            _bullet.Update();

            CheckScreenSize();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        
        /// <summary>
        /// Функция проверки размера игровой формы. Если размер формы < 0 или > 1000 по шиоине или высоте - выбрасывает исключение
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static void CheckScreenSize()
        {
            //получаем текущие размеры формы
            int currentWidth = Form1.ActiveForm.Width;
            int currntHeight = Form1.ActiveForm.Height;
            //Если размер формы не соответствует заданным выбасываем исключение
            if (currentWidth > 1000 || currentWidth < 0 || currntHeight > 1000 || currntHeight < 0)
            {
             throw new ArgumentOutOfRangeException();
            }
        }
    }
}