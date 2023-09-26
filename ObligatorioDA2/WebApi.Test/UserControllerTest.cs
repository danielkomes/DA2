using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models.In;
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

        [TestMethod]
        public void GetOk()
        {
            User user1 = new User()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            IEnumerable<User> users = new List<User> { user1 };
            IEnumerable<UserModelOut> userModelOuts = new List<UserModelOut> { new UserModelOut(user1) };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(user1);


            IActionResult actual = userController.Get("test@test.com");
            IActionResult expected = new OkObjectResult(new UserModelOut(user1));
            Assert.AreEqual(expected.GetType(), actual.GetType());

            OkObjectResult actualOk = actual as OkObjectResult;
            UserModelOut actualModel = actualOk.Value as UserModelOut;
            OkObjectResult expectedOk = expected as OkObjectResult;
            UserModelOut expectedModel = expectedOk.Value as UserModelOut;
            Assert.AreEqual(expectedModel.Email, actualModel.Email);
            Assert.AreEqual(expectedModel.Address, actualModel.Address);
            userMock.VerifyAll();
        }

        [TestMethod]
        public void PostOk()
        {
            UserModelIn userModel = new UserModelIn()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object);
            userMock.Setup(m => m.Add(It.IsAny<User>()));

            IActionResult actual = userController.Post(userModel);
            IActionResult expected = new CreatedResult(userModel.Email, "User created");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            CreatedResult actualOk = actual as CreatedResult;
            CreatedResult expectedOk = expected as CreatedResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            userMock.VerifyAll();
        }

        [TestMethod]
        public void PutOk()
        {
            UserModelIn userModel = new UserModelIn()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object);
            userMock.Setup(m => m.Update(It.IsAny<User>()));

            IActionResult actual = userController.Put(userModel.Email, userModel);
            IActionResult expected = new OkObjectResult("User modified");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            userMock.VerifyAll();
        }
    }
}