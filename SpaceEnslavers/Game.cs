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

        static Game()
        { 
        }

        public static BaseObject[] _objs;

        public static void Load()
        {

            _objs = new BaseObject[40];

            // нарисовали планету
            _objs[1] = new Planet(new Point(20, 20), new Point(10, 10), new Size(5, 5), "Earth");
            
            // отрисовали фон
            _objs[0] = new Space(new Point(0, 0), new Point(0, 0), new Size(5, 5));

            // отрисовываем звезды 
            for (int i = 2; i < _objs.Length/2; i++)
            {
                _objs[i] = new Star(new Point(0, i*20), new Point(-i, i), new Size(5, 5));
            }

            //// рисуем кружки 
            for (int i = _objs.Length / 2; i < _objs.Length ; i++)
                _objs[i] = new BaseObject(new Point(0, i * 20), new Point(-i, -i), new Size(10, 10));
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
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }

            Buffer.Render();

        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
            }

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}