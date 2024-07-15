using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstar_C224
{
    internal class Weapon
    {
        int bullet;

        public Weapon(int bullet)
        {
            this.bullet = bullet;
        }

        public void Shoot()
        {
            if (bullet > 0)
            {
                bullet--;
                Console.WriteLine("Бах!");
            }
            else
            {
                Console.WriteLine("Неудачка...");
            }
        }
        public void Reload(int bullet)
        {
            this.bullet = bullet;
        }
        public bool IsLoaded() 
        {  
            return bullet > 0; 
        }
    }
}
