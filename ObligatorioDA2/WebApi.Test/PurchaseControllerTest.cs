using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace WebApi.Test
{
    [TestClass]
    public class PurchaseControllerTest
    {
        private Mock<IPurchaseLogic> PurchaseMock { get; set; }
        private PurchaseController PurchaseController { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            PurchaseMock = new Mock<IPurchaseLogic>(MockBehavior.Strict);
            PurchaseController = new PurchaseController(PurchaseMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PurchaseMock.VerifyAll();
        }

        [TestMethod]
        public void GetPurchasesOk()
        {
            Purchase purchase = new Purchase()
            {

            };
            IEnumerable<Purchase> purchases = new List<Purchase>() { purchase };
            PurchaseMock.Setup(m => m.GetAll()).Returns(purchases);

            IActionResult actual = PurchaseController.GetAll();
            IActionResult expected = new OkObjectResult(purchases);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            PurchaseMock.VerifyAll();
        }
    }
}
