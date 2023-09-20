using Domain;
using IBusinessLogic;

namespace BusinessLogic.Test
{
    [TestClass]
    public class Promotion3x2Test
    {
        [TestMethod]
        public void TestNoProducts()
        {
            IPromotion p = new Promotion3x2();
            IEnumerable<Product> products = new List<Product>();
            float total = p.GetTotal(products);
            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void Test1Product()
        {
            IPromotion p = new Promotion3x2();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            products.Add(p1);
            float total = p.GetTotal(products);
            Assert.AreEqual(100, total);
        }

        [TestMethod]
        public void Test2Products()
        {
            IPromotion p = new Promotion3x2();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat1"
            };
            products.Add(p1);
            products.Add(p2);
            float total = p.GetTotal(products);
            float expected = 100 + 200;
            Assert.AreEqual(expected, total);
        }

        [TestMethod]
        public void Test3Products()
        {
            IPromotion p = new Promotion3x2();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat1"
            };
            Product p3 = new Product()
            {
                Price = 300,
                Category = "cat1"
            };
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            float total = p.GetTotal(products);
            float expected = 200 + 300;
            Assert.AreEqual(expected, total);
        }

        [TestMethod]
        public void Test3Products3DifferentCategories()
        {
            IPromotion p = new Promotion3x2();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat2"
            };
            Product p3 = new Product()
            {
                Price = 300,
                Category = "cat3"
            };
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            float total = p.GetTotal(products);
            float expected = 100 + 200 + 300;
            Assert.AreEqual(expected, total);
        }

        [TestMethod]
        public void Test3Products2DifferentCategories()
        {
            IPromotion p = new Promotion3x2();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat1"
            };
            Product p3 = new Product()
            {
                Price = 300,
                Category = "cat2"
            };
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            float total = p.GetTotal(products);
            float expected = 100 + 200 + 300;
            Assert.AreEqual(expected, total);
        }
    }
}