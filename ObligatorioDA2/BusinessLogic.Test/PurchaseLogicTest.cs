using Domain;
using IDataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PurchaseLogicTest
    {
        private Mock<IService<Purchase>> PurchaseMock { get; set; }
        private PurchaseLogic PurchaseLogic { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            PurchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            PurchaseLogic = new PurchaseLogic(PurchaseMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PurchaseMock.VerifyAll();
        }

        [TestMethod]
        public void GetAll1Purchase()
        {
            User user1 = new User()
            {
                Email = "user@test.com"
            };
            Product product1 = new Product()
            {
                Name = "product1"
            };
            IEnumerable<Product> products = new List<Product>() { product1 };
            Purchase purchase1 = new Purchase()
            {
                User = user1,
                Products = products
            };
            IEnumerable<Purchase> purchases = new List<Purchase>() { purchase1 };

            PurchaseMock.Setup(m => m.GetAll()).Returns(purchases);

            IEnumerable<Purchase> actual = PurchaseLogic.GetAll();
            IEnumerable<Purchase> expected = purchases;

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Id, actual.ElementAt(i).Id);
            }
        }
    }
}
