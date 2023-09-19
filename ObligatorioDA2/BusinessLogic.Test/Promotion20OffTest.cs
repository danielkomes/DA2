using Domain;
using IBusinessLogic;

namespace BusinessLogic.Test
{
    [TestClass]
    public class Promotion20OffTest
    {
        [TestMethod]
        public void TestNoProducts()
        {
            IPromotion p = new Promotion20Off();
            IEnumerable<Product> products = new List<Product>();
            float total = p.GetTotal(products);
            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void Test1Product()
        {
            IPromotion p = new Promotion20Off();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100
            };
            products.Add(p1);
            float total = p.GetTotal(products);
            Assert.AreEqual(100, total);
        }

        [TestMethod]
        public void Test2Products()
        {
            IPromotion p = new Promotion20Off();
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100
            };
            Product p2 = new Product()
            {
                Price = 200
            };
            products.Add(p1);
            products.Add(p2);
            float total = p.GetTotal(products);
            float expected = 100 + 200 - 200 * 0.2f;
            Assert.AreEqual(expected, total);
        }



    }
}