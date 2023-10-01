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

            var expectedObject = new
            {
                result = "Logged in",
                token = token
            };
            IActionResult actual = sessionController.Login("email@test.com");
            IActionResult expected = new OkObjectResult(expectedObject);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
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
