using Domain;
using IBusinessLogic;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class Promotion20OffTest
    {
        [TestMethod]
        public void TestNoProducts()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion20Off(pEntity);
            IEnumerable<Product> products = new List<Product>();
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(0, false, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test1Product()
        {
            PromotionEntity pEntity = new PromotionEntity();
            PromotionAbstract p = new Promotion20Off(pEntity);
            List<Product> products = new List<Product>();
            Product p1 = new Product()
            {
                Price = 100
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
            PromotionAbstract p = new Promotion20Off(pEntity);
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
            PromotionResult actual = p.GetTotal(products);
            PromotionResult expected = new PromotionResult(total, true, pEntity.Id);
            Assert.AreEqual(expected, actual);
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
            PromotionResult expected = new PromotionResult(total, true, pEntity.Id);
            Assert.AreEqual(expected, actual);
        }


    }
}