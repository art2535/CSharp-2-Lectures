using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopClassLibrary_C224;
using System;

namespace ShopUnitTestProject1_C224
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void ConstructorDefault()
        {
            Product product = new Product();
            Assert.AreEqual(product.Id, 1);
            Assert.AreEqual(product.Name, "Товар 1");
            Assert.AreEqual(product.Unit, Units.package);
            Assert.AreEqual(product.Category, Categories.Прочее);
        }
        [TestMethod]
        public void ConstructorNameCountPrice()
        {
            Product product = new Product("Новый товар", 10, 123);
            Assert.AreEqual(product.Id, 2);
            Assert.AreEqual(product.Name, "Новый товар");
            Assert.AreEqual(product.Unit, Units.package);
            Assert.AreEqual(product.Category, Categories.Прочее);
            Assert.AreEqual(product.Count, 10);
            Assert.AreEqual(product.Price, 123);
            Assert.AreEqual(product.Summa, 1230);
        }
        [TestMethod]
        public void ConstructorOrder()
        {
            Order order = new Order();
            Assert.AreEqual(order.Id, 1);
            Assert.IsNotNull(order.Products);
        }
        [TestMethod]
        public void ConstructorStock()
        {
            Stock stock = new Stock();
            Assert.IsNotNull(stock.Products);
        }
    }
}
