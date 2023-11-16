using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace WebApi.Test
{
    [TestClass]
    public class SessionControllerTest
    {
        private Mock<ISessionLogic> SessionMock { get; set; }
        private SessionController SessionController { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            SessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            SessionController = new SessionController(SessionMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            SessionMock.VerifyAll();
        }

        [TestMethod]
        public void LoginOk()
        {
            Guid token = new Guid();
            SessionMock.Setup(m => m.Authenticate(It.IsAny<User>())).Returns(token);

            var expectedObject = new
            {
                result = "Logged in",
                token = token
            };
            IActionResult actual = SessionController.Login("email@test.com", "pass1");
            IActionResult expected = new OkObjectResult(expectedObject);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
        }

        [TestMethod]
        public void LogoutOk()
        {
            SessionMock.Setup(m => m.Logout());

            IActionResult actual = SessionController.Logout();
            IActionResult expected = new OkResult();

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

    }
}
