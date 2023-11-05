using Domain;
using IBusinessLogic;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PromotionTotalLookTest
    {
        [TestMethod]
        public void TestNoProducts()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new PromotionTotalLook(pEntity);
            IEnumerable<Product> products = new List<Product>();
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(0, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test1Product()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new PromotionTotalLook(pEntity);
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Colors = new List<string> { "red" }
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
            PromotionAbstract p = new PromotionTotalLook(pEntity);
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
            float total = 100 + 200;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test3ProductsSameColor()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new PromotionTotalLook(pEntity);
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
            float total = 100 + 200 + 300 - 300 * 0.5f;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test3Products1ColorDifferent()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new PromotionTotalLook(pEntity);
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
                Colors = new List<string> { "blue" }
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
        public void Test3Products1ColorExtra()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new PromotionTotalLook(pEntity);
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
                Colors = new List<string> { "blue", "red" }
            };
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            float total = 100 + 200 + 300 - 300 * 0.5f;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test3Products3Colors()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new PromotionTotalLook(pEntity);
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Colors = new List<string> { "red", "blue" }
            };
            Product p2 = new Product()
            {
                Price = 200,
                Colors = new List<string> { "red", "yellow" }
            };
            Product p3 = new Product()
            {
                Price = 300,
                Colors = new List<string> { "green", "red" }
            };
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            float total = 100 + 200 + 300 - 300 * 0.5f;
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test3Products3DifferentColors()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new PromotionTotalLook(pEntity);
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Colors = new List<string> { "red", "green", "blue" }
            };
            Product p2 = new Product()
            {
                Price = 200,
                Colors = new List<string> { "purple", "yellow", "black" }
            };
            Product p3 = new Product()
            {
                Price = 300,
                Colors = new List<string> { "gray", "orange", "white" }
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