using ShopClassLibrary_C224;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary1
{
    public static class DataBaseProduct
    {
        public static Stock Stock { get; }
        static DataBaseProduct()
        {
            Stock = new Stock();
            Stock.Add(new Product("Телефон 1", 20, 200000));
            Stock.Add(new Product
            {
                Name = "Телефон 2",
                Count = 22,
                Description = "Новая модель телефонов",
                ImagePath = "Images\\айфон.jpg",
                Price = 1234567,
                Category = Categories.Телефоны,
                Unit = Units.package,
                Delivery = true,
                ReceiptDate = DateTime.Now.AddDays(-10)
            });
            Stock.Add(new Product());
        }

        public static IEnumerable GetCategories()
        {
            List<string> categories = new List<string>();
            categories.Add("Все категории");
            categories.AddRange(Enum.GetNames(typeof(Categories)).ToList());
            return categories;
        }
    }
}
