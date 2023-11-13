using Domain;
using IBusinessLogic;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PromotionTotalLookTest
    {
        private PromotionAbstract PromotionTotalLook { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            PromotionEntity entity = new PromotionEntity()
            {
                Type = EPromotionType.PromotionTotalLook
            };
            PromotionTotalLook = new PromotionTotalLook(entity);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TestNoProducts()
        {
            IEnumerable<Product> products = new List<Product>();
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(0, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test1Product()
        {
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100,
                Colors = new List<string> { "red" }
            };
            products.Add(p1);
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(100, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test2Products()
        {
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
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3ProductsSameColor()
        {
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
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products1ColorDifferent()
        {
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
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products1ColorExtra()
        {
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
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products3Colors()
        {
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
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products3DifferentColors()
        {
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
            PromotionResult actual = PromotionTotalLook.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

    }
}