using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models.In;

namespace WebApi.Test
{
    [TestClass]
    public class SignupControllerTest
    {
        private Mock<ISignupLogic> SignupMock { get; set; }
        private SignupController SignupController { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            SignupMock = new Mock<ISignupLogic>(MockBehavior.Strict);
            SignupController = new SignupController(SignupMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            SignupMock.VerifyAll();
        }

        [TestMethod]
        public void SignupOk()
        {
            Guid token = new Guid();
            UserModelInForCustomers model = new UserModelInForCustomers()
            {
                Email = "user@test.com",
                Address = "user address"
            };
            SignupMock.Setup(m => m.Signup(It.IsAny<User>()));

            IActionResult actual = SignupController.Signup(model);
            IActionResult expected = new CreatedResult(model.Email, "User created");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            CreatedResult actualOk = actual as CreatedResult;
            CreatedResult expectedOk = expected as CreatedResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            SignupMock.VerifyAll();
        }
    }
}
