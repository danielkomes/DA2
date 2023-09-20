using Domain;
using IBusinessLogic;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PromotionTotalLookTest
    {
        [TestMethod]
        public void TestNoProducts()
        {
            IPromotion p = new PromotionTotalLook();
            IEnumerable<Product> products = new List<Product>();
            float total = p.GetTotal(products);
            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void Test1Product()
        {
            IPromotion p = new PromotionTotalLook();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Colors = new List<string> { "red" }
            };
            products.Add(p1);
            float total = p.GetTotal(products);
            Assert.AreEqual(100, total);
        }

        [TestMethod]
        public void Test2Products()
        {
            IPromotion p = new PromotionTotalLook();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Colors = new List<string> { "red" }
            };
            Product p2 = new Product()
            {
                Price = 200,
                Colors = new List<string> { "red" }
            };
            products.Add(p1);
            products.Add(p2);
            float total = p.GetTotal(products);
            float expected = 100 + 200;
            Assert.AreEqual(expected, total);
        }

        [TestMethod]
        public void Test3ProductsSameColor()
        {
            IPromotion p = new PromotionTotalLook();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Colors = new List<string> { "red" }
            };
            Product p2 = new Product()
            {
                Price = 200,
                Colors = new List<string> { "red" }
            };
            Product p3 = new Product()
            {
                Price = 300,
                Colors = new List<string> { "red" }
            };
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            float total = p.GetTotal(products);
            float expected = 100 + 200 + 300 - 300 * 0.5f;
            Assert.AreEqual(expected, total);
        }

    }
}