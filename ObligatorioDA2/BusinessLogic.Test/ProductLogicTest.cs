using Domain;
using IBusinessLogic;
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
    public class ProductLogicTest
    {
        private Mock<IService<Product>> ProductMock { get; set; }
        private ProductLogic ProductLogic { get; set; }
        [TestInitialize]
        public void TestInitialize()
        {
            ProductMock = new Mock<IService<Product>>(MockBehavior.Strict);
            ProductLogic = new ProductLogic(ProductMock.Object);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            ProductMock.VerifyAll();
        }

        [TestMethod]
        public void GetAll3Products()
        {
            Product p1 = new Product()
            {
                Name = "p1",
            };
            Product p2 = new Product()
            {
                Name = "p2",
            };
            Product p3 = new Product()
            {
                Name = "p3",
            };
            IEnumerable<Product> products = new List<Product>() { p1, p2, p3 };
            ProductMock.Setup(m => m.GetAll()).Returns(products);

            IEnumerable<Product> actual = ProductLogic.GetAll();

            for (int i = 0; i < products.Count(); i++)
            {
                Assert.AreEqual(products.ElementAt(i).Name, actual.ElementAt(i).Name);
            }
        }
    }

}
