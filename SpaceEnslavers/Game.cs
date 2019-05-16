using SpaceEnslavers.Objects;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Collections.Generic;

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
        public static List<Bullet> _Bullets = new List<Bullet>();

        //массивчик с астероидами
        private static Asteroid[] _asteroids;

        public static Timer _timer = new Timer();

        // аптечка
        public static FirstAidKit _firstAidKit;

        //счетчик уничтоженных астероидов
        public static int DestroyedAsteroids { get; set; } = 0;

        //Создаем космический корабль
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(30, 30), "SmallShip");

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
            //   _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));

            //создадим астероиды
            _asteroids = new Asteroid[3];

            //создадим аптечку
            _firstAidKit = new FirstAidKit(new Point(Game.Width, 200), new Point(5, 0), new Size(20, 20));

            var rnd = new Random();

            // нарисуем летающие звезды 
            for (int i = 2; i < _objs.Length; i++)
            {
                int random = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(20, rnd.Next(0, Game.Height)), new Point(-random, random),
                    new Size(3, 3));
            }

            for (var i = 0; i < _asteroids.Length; i++)
            {
                int random = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(100, rnd.Next(0, Game.Height)), new Point(-random / 5, random),
                    new Size(random, random));
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

            //Timer timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_Tick;

            // Обработчик событий нажатия на кнопку, вызывающий при нажатии метод Form_KeyDown
            form.KeyDown += Form_KeyDown;
            // подписались на делегата MessageDie в классе Ship. Когда у корабля будет 0 жизней выховется метод Die в котором сработает событие MessageDie и мы выполним метод Finish 
            Ship.MessageDie += Finish;
            //При возникновении события AsteroidDie выполнится метод AsteroidDieLog
            Asteroid.AsteroidDie += AsteroidDieLog;
        }

        /// <summary>
        /// Метод обработки нажатий на кнопки, вызывается обработчиком событй и выполняет различные действия в зависимости от того, какая кнопка нажата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //При нажатии Ctrl астероид выпускает снаряд
            if (e.KeyCode == Keys.ControlKey)
            {
                _Bullets.Add(new Bullet(new Point(_ship.Rectangle.X + 20, _ship.Rectangle.Y + 14), new Point(4, 0),
                    new Size(4, 1)));
                SystemSounds.Beep.Play();
            }

            //При нажатии стрелки вверх выполнился метод Up экземпляра класса ship
            if (e.KeyCode == Keys.Up)
            {
                _ship.Up();
            }

            //При нажатии стрелки вниз выполнился метод Down экземпляра класса ship
            if (e.KeyCode == Keys.Down)
            {
                _ship.Down();
            }
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
            foreach (var bullet in _Bullets)
            {
                bullet.Draw();
            }

            //отрисовали все астероиды
            foreach (Asteroid item in _asteroids)
            {
                item?.Draw();
            }

            //TODO: Менять кораболь когда собьешь определенное количество астероидов 
//            if (DestroyedAsteroids == 1)
//            {
//                _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(30, 30), "MiddleShip");
//            }

            //отрисовали корабль
            _ship.Draw();

            //отрисовка аптечки
            _firstAidKit.Draw();

            //отрисовали здоровье корабля
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy: " + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 800, 20);
                Buffer.Graphics.DrawString("Asteroids: " + DestroyedAsteroids, SystemFonts.DefaultFont,
                    Brushes.White, 800, 40);
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

            //если корабль столкнулся с аптечкой и у него меньше 100 здоробья - аптечка лечит кораболь. Сама аптечка потом регенерируется в любом месте
            if (_firstAidKit.Collision(_ship))
            {
                var rnd = new Random();
                _ship.EnergyRecovery(10);
                _firstAidKit = new FirstAidKit(new Point(Game.Width, rnd.Next(20, Height - 10)), new Point(5, 0),
                    new Size(20, 20));
            }

            _firstAidKit.Update();

            for (var i = 0; i < _asteroids.Length; i++)
            {
                var rnd = new Random();

                if (_asteroids[i] == null)
                {
                    continue;
                }

                _asteroids[i].Update();

                for (int j = 0; j < _Bullets.Count; j++)
                {
                    if (_asteroids[i] != null && _Bullets[j].Collision(_asteroids[i]))
                    {
                        SystemSounds.Hand.Play();
                        //Если снаряд попал в астероид рисуем новый рандомный астероид
                        int random = rnd.Next(5, 50);
                        _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)),
                            new Point(-random / 5, random), new Size(random, random));
                        //добавим в счетчик астероидов сбитый астероид
                        DestroyedAsteroids++;

                        //вызовем метод который вызовет событие гибели астероида на которое мы подпишемся
                        _asteroids[i].Die();
                        //Удалим из очереди снарядов тот снаряд, который попал в астероид                      
                        _Bullets.RemoveAt(j);
                        j--;
                    }
                }


                if (!_ship.Collision(_asteroids[i]))
                {
                    continue;
                }

                // при столкновении астероида с кораблем, вычитаем у корабля от 1 до 10 жизней
                var damage = rnd.Next(1, 10);
                _ship?.EnergyLow(damage);
                SystemSounds.Asterisk.Play();
                //занесем информацию о столкновении в консоль
                _ship?.ShipDamageLog(damage);

                //если у коробля заканчиваются жизни он умирает
                if (_ship.Energy <= 0)
                {
                    //когда корабль разбился, обнулим счетчик сбитых астероидов
                    DestroyedAsteroids = 0;
                    _ship?.Die();
                }
            }

            //Обновляем позицию снаряда
            foreach (var bullet in _Bullets)
            {
                bullet.Update();
            }

            //обновляем позицию аптечки

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

        /// <summary>
        /// Метод, вызываемый если корабль погиб и игра окончена
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Console.WriteLine("Корабль уничтожен");
            Buffer.Graphics.DrawString("THE END", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
                Brushes.White, 300, 150);
            Buffer.Render();
        }

        //логируем в консоли гибель астероидов
        public static void AsteroidDieLog()
        {
            Console.WriteLine("Астероид уничтожен");
        }
    }
}