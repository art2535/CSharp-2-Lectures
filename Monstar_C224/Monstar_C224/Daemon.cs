using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstar_C224
{
    internal class Daemon : Monster
    {
        int brain;
        public void Think()
        {
            Console.WriteLine("Демон " + Name);
            for (int i = 0; i < brain; i++)
            {
                Console.Write("думает, ");
            }
            Console.WriteLine("...");
        }
        public Daemon()
        {
            brain = 3;
            weapon = new Weapon(3);
        }
        public Daemon(string name, int brain) : this()
        {
            Name = name;
            this.brain = brain;
        }
        public override void Draw()
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            switch (color)
            {
                case Color.Blue: Console.ForegroundColor = ConsoleColor.Blue; break;
                case Color.Green: Console.ForegroundColor = ConsoleColor.Green; break;
                case Color.Red: Console.ForegroundColor = ConsoleColor.Red; break;
                default: Console.ForegroundColor = ConsoleColor.Black; break;
            }
            Console.WriteLine($"Демон {Name} ({id}) имеет силу {Force}, жизнь {Life} и мощь {Power}, ум {brain}, {Status()}");
            Console.ForegroundColor = currentColor;
        }
        Weapon weapon;
        public void Bax(int countBax)
        {
            for (int i = 0; i < countBax; i++)
            {
                weapon.Shoot();
            }
        }
        public void ReloadWeapon(int bullet)
        {
            weapon.Reload(bullet);
        }
        public bool IsLoadWeapon() 
        {  
            return (weapon != null && weapon.IsLoaded()); 
        }
    }
}