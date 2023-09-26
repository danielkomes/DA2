using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models.Out;

namespace WebApi.Test
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void GetAll1User()
        {
            User user1 = new User();
            List<User> users = new List<User> { user1 };
            List<UserModelOut> userModelOuts = new List<UserModelOut> { new UserModelOut(user1) };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object);
            userMock.Setup(m => m.GetAll()).Returns(users);


            IActionResult actual = userController.GetAll();
            IActionResult expected = new OkObjectResult(userModelOuts);
            Assert.AreEqual(expected.GetType(), actual.GetType());

            OkObjectResult actualOk = actual as OkObjectResult;
            List<UserModelOut> actualModels = actualOk.Value as List<UserModelOut>;
            OkObjectResult expectedOk = expected as OkObjectResult;
            List<UserModelOut> expectedModels = expectedOk.Value as List<UserModelOut>;
            for (int i = 0; i < userModelOuts.Count(); i++)
            {
                Assert.AreEqual(expectedModels[i].Email, actualModels[i].Email);
                Assert.AreEqual(expectedModels[i].Address, actualModels[i].Address);
            }
        }
    }
}