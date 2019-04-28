using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEnslavers.Interfaces
{
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rectangle { get; }
    }
}
