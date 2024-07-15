using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstar_C224
{
    internal class Hunter : Spirit, IMovable
    {
        int speed;
        public int Speed 
        { 
            get => throw new NotImplementedException(); 
            set
            {
                if (value < IMovable.MinSpeed)
                    speed = IMovable.MinSpeed;
                else
                {
                    if (value > IMovable.MaxSpeed)
                        speed = IMovable.MaxSpeed;
                    else
                        speed = value;
                }
                
            }
        }
        public override void Draw()
        {
            Console.WriteLine("Охотник");
        }
        public void Move()
        {
            Console.WriteLine("Я иду со скоростью " + speed.ToString());
        }
    }
}
