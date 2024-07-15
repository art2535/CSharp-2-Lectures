using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstar_C224
{
    internal interface IMovable
    {
        const int MinSpeed = 0;
        const int MaxSpeed = 1;
        int Speed {  get; set; }
        void Move();
    }
}
