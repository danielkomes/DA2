using Domain;
using IBusinessLogic;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class Promotion20OffTest
    {
        private PromotionAbstract Promotion20Off { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            PromotionEntity entity = new PromotionEntity()
            {
                Type = EPromotionType.Promotion20Off
            };
            Promotion20Off = new Promotion20Off(entity);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TestNoProducts()
        {
            IEnumerable<Product> products = new List<Product>();
            PromotionResult actual = Promotion20Off.GetTotal(products);
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
                Price = 100
            };
            products.Add(p1);

            PromotionResult actual = Promotion20Off.GetTotal(products);
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
                Price = 100
            };
            Product p2 = new Product()
            {
                Price = 200
            };
            products.Add(p1);
            products.Add(p2);

            float total = 100 + 200 - 200 * 0.2f;
            PromotionResult actual = Promotion20Off.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }

        [TestMethod]
        public void Test3Products()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract promotion = new Promotion20Off(pEntity);
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100
            };
            Product p2 = new Product()
            {
                Price = 200
            };
            Product p3 = new Product()
            {
                Price = 300
            };
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);

            float total = 100 + 200 + 300 - 300 * 0.2f;
            PromotionResult actual = promotion.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }


    }
}