using System.Windows.Forms;

namespace SpaceEnslavers
{
    partial class GameObjectException
    {
        public GameObjectException(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}