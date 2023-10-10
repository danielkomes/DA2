using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models.In;

namespace WebApi.Test
{
    [TestClass]
    public class SignupControllerTest
    {
        [TestMethod]
        public void SignupOk()
        {
            Guid token = new Guid();
            UserModelInForCustomers model = new UserModelInForCustomers()
            {
                Email = "user@test.com",
                Address = "user address"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            SignupController signupController = new SignupController(userMock.Object);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(false);
            userMock.Setup(m => m.Add(It.IsAny<User>()));

            var expectedObject = new
            {
                result = "Logged in",
                token = token
            };
            IActionResult actual = signupController.Signup(model);
            IActionResult expected = new CreatedResult(model.Email, "User created");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            CreatedResult actualOk = actual as CreatedResult;
            CreatedResult expectedOk = expected as CreatedResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            userMock.VerifyAll();
        }
        [TestMethod]
        public void SignupEmailAlreadyExists()
        {
            Guid token = new Guid();
            UserModelInForCustomers model = new UserModelInForCustomers()
            {
                Email = "user@test.com",
                Address = "user address"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            SignupController signupController = new SignupController(userMock.Object);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(true);

            var expectedObject = new
            {
                result = "Logged in",
                token = token
            };
            IActionResult actual = signupController.Signup(model);
            IActionResult expected = new ObjectResult("Email already exists");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            ObjectResult actualOk = actual as ObjectResult;
            ObjectResult expectedOk = expected as ObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            userMock.VerifyAll();
        }
    }
}
