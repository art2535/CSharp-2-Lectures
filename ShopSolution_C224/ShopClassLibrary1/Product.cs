using System;
namespace ShopClassLibrary_C224
{
    public class Product
    {
        static int CountProduct;
        public int Id { get; }
        public string Name { get; set; }
        public int Price {  get; set; }
        public decimal PriceRub { get { return Price / (decimal)100; } }
        public Categories Category { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
        public Units Unit {  get; set; }
        public bool Delivery {  get; set; }
        public DateTime ReceiptDate { get; set; }
        public int Summa { get { return Convert.ToInt32(Price * Count); } }
        public string ImagePath { get; set; }
        public Product()
        {
            CountProduct++;
            Id = CountProduct;
            Name = "Товар " + Id;
            Category = Categories.Прочее;
            Unit = Units.package;
            ReceiptDate = DateTime.Now.Date;
            ImagePath = @"Images\nophoto.jpg";
        }
        public Product(string name, int count, int price) : this()
        {
            Name = name;
            Count = count;
            Price = price;
        }
    }
}
