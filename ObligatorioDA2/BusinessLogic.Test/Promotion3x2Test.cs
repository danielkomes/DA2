using Domain;
using IBusinessLogic;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class Promotion3x2Test
    {
        [TestMethod]
        public void TestNoProducts()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion3x2(pEntity);
            IEnumerable<Product> products = new List<Product>();
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(0, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test1Product()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion3x2(pEntity);
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            products.Add(p1);
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(100, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test2Products()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion3x2(pEntity);
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
            products.Add(p2);
            products.Add(p1);
            float total = 100 + 200;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test3Products()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion3x2(pEntity);
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
            float total = 200 + 300;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test3Products3DifferentCategories()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion3x2(pEntity);
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
            float total = 100 + 200 + 300;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test3Products2DifferentCategories()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion3x2(pEntity);
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
            float total = 100 + 200 + 300;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }
    }
}