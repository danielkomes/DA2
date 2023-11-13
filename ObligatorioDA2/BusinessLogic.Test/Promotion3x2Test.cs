using Domain;
using IBusinessLogic;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class Promotion3x2Test
    {
        private PromotionAbstract Promotion3x2 { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            PromotionEntity entity = new PromotionEntity()
            {
                Type = EPromotionType.Promotion3x2
            };
            Promotion3x2 = new Promotion3x2(entity);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TestNoProducts()
        {
            IEnumerable<Product> products = new List<Product>();

            PromotionResult actual = Promotion3x2.GetTotal(products);
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
                Category = "cat1"
            };
            products.Add(p1);

            PromotionResult actual = Promotion3x2.GetTotal(products);
            PromotionResult expected = new PromotionResult(100, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
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
            PromotionResult expected = new PromotionResult(total, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products()
        {
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
            PromotionResult actual = Promotion3x2.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products3DifferentCategories()
        {
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
            PromotionResult actual = Promotion3x2.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products2DifferentCategories()
        {
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
            PromotionResult actual = Promotion3x2.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, false);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }
    }
}