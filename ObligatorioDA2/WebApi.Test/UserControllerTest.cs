using DataAccess.Exceptions;
using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
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
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
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
            sessionMock.VerifyAll();
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
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(user1);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(user1);


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
            sessionMock.VerifyAll();
        }

        [TestMethod]
        public void GetUserBeingAdmin()
        {
            User user1 = new User()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            User admin = new User()
            {
                Email = "test@test.com",
                Address = "test 123",
                Roles = new List<EUserRole>() { EUserRole.Admin }
            };
            IEnumerable<User> users = new List<User> { user1 };
            IEnumerable<UserModelOut> userModelOuts = new List<UserModelOut> { new UserModelOut(user1) };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(user1);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(admin);


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
            sessionMock.VerifyAll();
        }

        [TestMethod]
        public void GetUserBeingAnother()
        {
            User target = new User()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            User current = new User()
            {
                Email = "test2@test2.com",
                Address = "test 456"
            };
            IEnumerable<User> users = new List<User> { target };
            IEnumerable<UserModelOut> userModelOuts = new List<UserModelOut> { new UserModelOut(target) };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(target);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(current);


            IActionResult actual = userController.Get("test@test.com");
            IActionResult expected = new ObjectResult("Profile mismatch");
            Assert.AreEqual(expected.GetType(), actual.GetType());

            ObjectResult actualOk = actual as ObjectResult;
            ObjectResult expectedOk = expected as ObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            userMock.VerifyAll();
            sessionMock.VerifyAll();
        }

        [TestMethod]
        public void GetInvalidUserBeingAnother()
        {
            User target = new User()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            User current = new User()
            {
                Email = "test2@test2.com",
                Address = "test 456"
            };
            IEnumerable<User> users = new List<User> { target };
            IEnumerable<UserModelOut> userModelOuts = new List<UserModelOut> { new UserModelOut(target) };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Throws(new ResourceNotFoundException("User not found"));
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(current);


            IActionResult actual = userController.Get("test@test.com");
            IActionResult expected = new ObjectResult("Profile mismatch");
            Assert.AreEqual(expected.GetType(), actual.GetType());

            ObjectResult actualOk = actual as ObjectResult;
            ObjectResult expectedOk = expected as ObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            userMock.VerifyAll();
            sessionMock.VerifyAll();
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
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(false);
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
        public void PostEmailAlreadyExists()
        {
            UserModelIn userModel = new UserModelIn()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(true);

            IActionResult actual = userController.Post(userModel);
            IActionResult expected = new ObjectResult("Email already exists") { StatusCode = StatusCodes.Status403Forbidden };

            Assert.AreEqual(expected.GetType(), actual.GetType());
            ObjectResult actualOk = actual as ObjectResult;
            ObjectResult expectedOk = expected as ObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            Assert.AreEqual(expectedOk.StatusCode, actualOk.StatusCode);
            userMock.VerifyAll();
        }

        [TestMethod]
        public void PutOk()
        {
            User current = new User()
            {
                Email = "user@test.com",
                Address = "user address"
            };
            UserModelIn userModel = new UserModelIn()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(current);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(false);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(current);
            userMock.Setup(m => m.Update(It.IsAny<User>()));

            IActionResult actual = userController.Put(current.Email, userModel);
            IActionResult expected = new OkObjectResult("User modified");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            sessionMock.VerifyAll();
            userMock.VerifyAll();
        }

        [TestMethod]
        public void PutAnotherUserBeingAdminOk()
        {
            User current = new User()
            {
                Email = "admin@admin.com",
                Address = "admin",
                Roles = new List<EUserRole> { EUserRole.Admin }
            };
            User target = new User()
            {
                Email = "user@test.com",
                Address = "user address",
            };
            UserModelIn updated = new UserModelIn()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(current);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(false);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(target);
            userMock.Setup(m => m.Update(It.IsAny<User>()));

            IActionResult actual = userController.Put(current.Email, updated);
            IActionResult expected = new OkObjectResult("User modified");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            sessionMock.VerifyAll();
            userMock.VerifyAll();
        }

        [TestMethod]
        public void PutBeingAnotherUser()
        {
            User current = new User()
            {
                Email = "user@test.com",
                Address = "user address",
            };
            User target = new User()
            {
                Email = "user2@test2.com",
                Address = "user2 address",
            };
            UserModelIn updated = new UserModelIn()
            {
                Email = "updated@test.com",
                Address = "updated address"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(current);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(false);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(target);

            IActionResult actual = userController.Put(current.Email, updated);
            IActionResult expected = new ObjectResult("Profile mismatch") { StatusCode = StatusCodes.Status403Forbidden };

            Assert.AreEqual(expected.GetType(), actual.GetType());
            ObjectResult actualOk = actual as ObjectResult;
            ObjectResult expectedOk = expected as ObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            Assert.AreEqual(expectedOk.StatusCode, actualOk.StatusCode);
            sessionMock.VerifyAll();
            userMock.VerifyAll();
        }

        [TestMethod]
        public void PutInvalidUserBeingAnotherUser()
        {
            User current = new User()
            {
                Email = "user@test.com",
                Address = "user address",
            };
            User target = new User()
            {
                Email = "user2@test2.com",
                Address = "user2 address",
            };
            UserModelIn updated = new UserModelIn()
            {
                Email = "updated@test.com",
                Address = "updated address"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(current);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(false);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Throws(new ResourceNotFoundException("User not found"));

            IActionResult actual = userController.Put(current.Email, updated);
            IActionResult expected = new ObjectResult("Profile mismatch") { StatusCode = StatusCodes.Status403Forbidden };

            Assert.AreEqual(expected.GetType(), actual.GetType());
            ObjectResult actualOk = actual as ObjectResult;
            ObjectResult expectedOk = expected as ObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            Assert.AreEqual(expectedOk.StatusCode, actualOk.StatusCode);
            sessionMock.VerifyAll();
            userMock.VerifyAll();
        }

        [TestMethod]
        public void PutEmailAlreadyExists()
        {
            User current = new User()
            {
                Email = "user@test.com",
                Address = "user address",
            };
            UserModelIn updated = new UserModelIn()
            {
                Email = "updated@test.com",
                Address = "updated address"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(current);
            userMock.Setup(m => m.Exists(It.IsAny<User>())).Returns(true);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(current);

            IActionResult actual = userController.Put(current.Email, updated);
            IActionResult expected = new ObjectResult("Email already exists") { StatusCode = StatusCodes.Status403Forbidden };

            Assert.AreEqual(expected.GetType(), actual.GetType());
            ObjectResult actualOk = actual as ObjectResult;
            ObjectResult expectedOk = expected as ObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            Assert.AreEqual(expectedOk.StatusCode, actualOk.StatusCode);
            sessionMock.VerifyAll();
            userMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteOk()
        {
            UserModelIn userModel = new UserModelIn()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserController userController = new UserController(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Delete(It.IsAny<User>()));

            IActionResult actual = userController.Delete(userModel.Email);
            IActionResult expected = new OkObjectResult("User deleted");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            userMock.VerifyAll();
        }
    }
}