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
    public class SessionControllerTest
    {
        [TestMethod]
        public void LoginOk()
        {
            Guid token = new Guid();
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            SessionController sessionController = new SessionController(sessionMock.Object);
            sessionMock.Setup(m => m.Authenticate(It.IsAny<User>())).Returns(token);


            IActionResult actual = sessionController.Login("email@test.com", "password123");
            IActionResult expected = new OkObjectResult("Logged in");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            sessionMock.VerifyAll();
        }

        [TestMethod]
        public void LogoutOk()
        {
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            SessionController sessionController = new SessionController(sessionMock.Object);
            sessionMock.Setup(m => m.Logout());


            IActionResult actual = sessionController.Logout();
            IActionResult expected = new OkObjectResult("Logged out");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            sessionMock.VerifyAll();
        }

    }
}
