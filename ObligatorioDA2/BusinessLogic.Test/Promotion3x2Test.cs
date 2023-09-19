using Domain;
using IBusinessLogic;

namespace BusinessLogic.Test
{
    [TestClass]
    public class Promotion3x2Test
    {
        [TestMethod]
        public void TestNoProducts()
        {
            IPromotion p = new Promotion3x2();
            IEnumerable<Product> products = new List<Product>();
            float total = p.GetTotal(products);
            Assert.AreEqual(0, total);
        }


    }
}