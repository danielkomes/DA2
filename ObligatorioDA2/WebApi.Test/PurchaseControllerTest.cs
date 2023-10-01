using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models.In;

namespace WebApi.Test
{
    [TestClass]
    public class PurchaseControllerTest
    {
        [TestMethod]
        public void GetPurchasesOk()
        {
            Purchase purchase = new Purchase()
            {

            };
            IEnumerable<Purchase> purchases = new List<Purchase>() { purchase };
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            PurchaseController purchaseController = new PurchaseController(purchaseMock.Object);
            purchaseMock.Setup(m => m.GetAll()).Returns(purchases);

            //var expectedObject = new
            //{
            //    result = "Logged in",
            //    token = token
            //};
            IActionResult actual = purchaseController.GetAll();
            IActionResult expected = new OkObjectResult(purchases);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            purchaseMock.VerifyAll();
        }
    }
}
