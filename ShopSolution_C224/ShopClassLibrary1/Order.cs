using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary_C224
{
    public class Order
    {
        static int countOrder;
        public int Id { get; }
        public List<Product> Products { get; }
        public int Summa
        {
            get
            {
                int summa = 0;
                foreach (var item in Products)
                    summa += item.Summa;
                return summa;
            }
        }
        public Order()
        {
            countOrder++;
            Id = countOrder;
            Products = new List<Product>();
        }
    }
}
