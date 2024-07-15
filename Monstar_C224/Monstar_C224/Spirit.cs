using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monstar_C224
{
    // От абстрактного класса нельзя сделать объекты
    internal abstract class Spirit
    {
        public string name;
        int life;
        // Статическое поле - общее для всех объектов класса обращение - по имени класса
        public static int count = 0;
        // Количество монстров, созданных в программе в любых местах
        protected Color color; // цвет монстра
        // поле только для чтения - установить значение можно только один раз в конструкторе
        protected readonly int id;    // id - поле только для чтения, это "уникальное" поле
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    return "Без имени";
                return name;
            }
            set { name = value; }
        }
        public virtual int Life
        {
            get { return life; }
            set
            {
                if (value < 0)
                    life = 0;
                else if (value > 100)
                    life = 100;
                else
                    life = value;
            }
        }
        // Количество монстров
        public static int Count 
        { 
            get { return count; } 
        }
        public abstract void Draw();
        public Spirit()
        {
            Name = "";
            Life = 100;
            color = Color.Green;
            count++;
            id = count;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}