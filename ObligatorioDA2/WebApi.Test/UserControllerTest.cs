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
            IEnumerable<User> users = new List<User> { user1 };
            IEnumerable<UserModelOut> userModelOuts = new List<UserModelOut> { new UserModelOut(user1) };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object);
            userMock.Setup(m => m.GetAll()).Returns(users);


            IActionResult actual = userController.GetAll();
            IActionResult expected = new OkObjectResult(userModelOuts);
            Assert.AreEqual(expected.GetType(), actual.GetType());

            OkObjectResult actualOk = actual as OkObjectResult;
            IEnumerable<UserModelOut> actualModels = actualOk.Value as IEnumerable<UserModelOut>;
            OkObjectResult expectedOk = expected as OkObjectResult;
            IEnumerable<UserModelOut> expectedModels = expectedOk.Value as IEnumerable<UserModelOut>;
            for (int i = 0; i < userModelOuts.Count(); i++)
            {
                Assert.AreEqual(expectedModels.ElementAt(i).Email, actualModels.ElementAt(i).Email);
                Assert.AreEqual(expectedModels.ElementAt(i).Address, actualModels.ElementAt(i).Address);
            }
            userMock.VerifyAll();
        }

        [TestMethod]
        public void GetAll3Users()
        {
            User user1 = new User();
            User user2 = new User();
            User user3 = new User();
            IEnumerable<User> users = new List<User>
            {
                user1,
                user2,
                user3
            };
            IEnumerable<UserModelOut> userModelOuts = new List<UserModelOut>
            {
                new UserModelOut(user1),
                new UserModelOut(user2),
                new UserModelOut(user3)
            };

            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object);
            userMock.Setup(m => m.GetAll()).Returns(users);


            IActionResult actual = userController.GetAll();
            IActionResult expected = new OkObjectResult(userModelOuts);
            Assert.AreEqual(expected.GetType(), actual.GetType());

            OkObjectResult actualOk = actual as OkObjectResult;
            IEnumerable<UserModelOut> actualModels = actualOk.Value as IEnumerable<UserModelOut>;
            OkObjectResult expectedOk = expected as OkObjectResult;
            IEnumerable<UserModelOut> expectedModels = expectedOk.Value as IEnumerable<UserModelOut>;
            for (int i = 0; i < userModelOuts.Count(); i++)
            {
                Assert.AreEqual(expectedModels.ElementAt(i).Email, actualModels.ElementAt(i).Email);
                Assert.AreEqual(expectedModels.ElementAt(i).Address, actualModels.ElementAt(i).Address);
            }
            userMock.VerifyAll();
        }
    }
}