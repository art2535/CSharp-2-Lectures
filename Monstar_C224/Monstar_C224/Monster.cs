using Monstar_C224;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Monstar_C224
{
    internal class Monster : Spirit
    {
        // Поля
        //double force;
        
        // Свойства
        
        public double Force { get; set; }
        public double Power
        {
            get { return Life * Force; }
        }

        // Методы
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
            Console.WriteLine($"Монстр {Name} ({id}) имеет силу {Force}, жизнь {Life} и мощь {Power}, {Status()}");
            Console.ForegroundColor = currentColor;
        }
        protected string Status()
        {
            if (Life > 80) return "Бодр и весел";
            if (Life > 50) return "Жив!";
            if (Life > 25)
            {
                color = Color.Blue;
                return "Скорее жив, чем мертв!";
            }
            if (Life > 10)
            {
                color = Color.Red;
                return "Скорее мертв, чем жив!";
            }
            if (Life > 0) return "Скоро умрет!";
            return "Умер!";
        }

        // Конструкторы
        public Monster(string name) : this()
        {
            Name = name;
        }
        public Monster()
        {
            Name = "";
            Life = 100;
            Force = 1;
            color = Color.Green;
            count++;
        }
        public Monster(string name, int life, double force) : this()
        {
            Name = name;
            Life = life;
            Force = force;
        }

        // Перегрузки операций
        public static Monster operator ++(Monster monster)
        {
            monster.Life++;
            return monster;
        }
        public static Monster operator --(Monster monster)
        {
            monster.Force--;
            return monster;
        }
        public static Monster operator +(int n, Monster monster)
        {
            monster.Life += n;
            return monster;
        }
        public static Monster operator +(Monster monster, int n)
        {
            monster.Life += n;
            return monster;
        }
        public static Monster operator +(Monster monster1, Monster monster2)
        {
            return new Monster(monster1.Name + "-" + monster2.Name, monster1.Life, monster2.Force);
        }
        public static bool operator >(Monster monster1, Monster monster2)
        {
            return monster1.Power > monster2.Power;
        }
        public static bool operator <(Monster monster1, Monster monster2)
        {
            return !(monster1 > monster2);
        }

        public static implicit operator int(Monster monster)
        {
            return (int)monster.Power;
        }
        public static explicit operator Monster(int num)
        {
            return new Monster("FromInt", num, num / 10);
        }
        public override string ToString()
        {
            return "Я - " + Name + ", " + Status();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType())
                return false;
            return Power == ((Monster)obj).Power;
        }
    }
}
