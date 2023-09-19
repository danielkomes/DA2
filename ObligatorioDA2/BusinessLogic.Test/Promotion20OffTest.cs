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



    }
}