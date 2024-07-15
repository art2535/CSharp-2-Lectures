using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace Monstar_C224
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 1;
            Monster Vasya = new Monster("Вася");
            Vasya.Draw();
            Monster Petya = new Monster("Петя");
            Petya.Life = 30;
            Petya.Draw();
            Petya.Draw();
            Monster Dasha = new Monster("Даша", 20, 3.5);
            Dasha.Draw();
            Dasha++;
            Dasha.Draw();
            Dasha--;
            Dasha.Draw();
            Dasha = Dasha + 10;
            Dasha.Draw();
            Dasha = 10 + Dasha;
            Dasha.Draw();
            Vasya += 20;
            Vasya.Draw();
            var Masha = Petya + Dasha;
            Masha.Draw();
            if (Masha > Petya)
                Console.WriteLine("Маша больше Пети");
            int num = Masha;
            Console.WriteLine($"int = {num}");
            Monster m = (Monster)num;
            m.Draw();
            Console.WriteLine($"Количество = {Monster.Count}");
            Daemon Dima = new Daemon("Дима", 5);
            Dima.Draw();
            Dima.Think();
            var Denis = new Daemon("Денис", 3);
            Gin Gin = new Gin();
            Hunter Hunter = new Hunter();
            Console.WriteLine("======= СТАДО =======");
            Spirit[] stado = new Spirit[] { Vasya, Petya, Dasha, Masha, m, Dima, Denis, Gin, Hunter };
            foreach (var monster in stado)
            {
                monster.Draw();
                if(monster is Daemon)
                {
                    Daemon daemon = (Daemon)monster;
                    daemon.Bax(2);
                    if (!daemon.IsLoadWeapon())
                    {
                        daemon.ReloadWeapon(5);
                    }
                }
                if(monster is IMovable)
                {
                    (monster as IMovable).Move();
                }
                if (monster.GetType() == typeof(Daemon))
                {
                    (monster as Daemon).Think();
                }
            }
            Console.WriteLine($"Количество = {Monster.Count}, {Dasha.ToString()}");
            Console.WriteLine(Vasya.Equals(Dima));
        }
    }
}