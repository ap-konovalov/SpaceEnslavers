using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceEnslavers
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Form form = new Form
            {
                //задаем высоту и ширину в половину от размера экрана
                Width = Screen.PrimaryScreen.Bounds.Width /2,
                Height = Screen.PrimaryScreen.Bounds.Height/2
            };
            Game.Init(form);
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);
        }
    }
}
