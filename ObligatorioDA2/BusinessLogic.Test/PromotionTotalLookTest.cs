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


    }
}