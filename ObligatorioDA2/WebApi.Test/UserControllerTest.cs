using Domain;
using IBusinessLogic;
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
        private Mock<IUserLogic> UserMock { get; set; }
        private UserController UserController { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UserMock = new Mock<IUserLogic>(MockBehavior.Strict);
            UserController = new UserController(UserMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            UserMock.VerifyAll();
        }

        [TestMethod]
        public void GetAll1User()
        {
            User user1 = new User();
            IEnumerable<User> users = new List<User> { user1 };
            IEnumerable<UserModelOutForAdmins> userModelOuts = new List<UserModelOutForAdmins> { new UserModelOutForAdmins(user1) };


            UserMock.Setup(m => m.GetAll()).Returns(users);


            IActionResult actual = UserController.GetAll();
            IActionResult expected = new OkObjectResult(userModelOuts);
            Assert.AreEqual(expected.GetType(), actual.GetType());

            OkObjectResult actualOk = actual as OkObjectResult;
            IEnumerable<UserModelOutForAdmins> actualModels = actualOk.Value as IEnumerable<UserModelOutForAdmins>;
            OkObjectResult expectedOk = expected as OkObjectResult;
            IEnumerable<UserModelOutForAdmins> expectedModels = expectedOk.Value as IEnumerable<UserModelOutForAdmins>;
            for (int i = 0; i < userModelOuts.Count(); i++)
            {
                Assert.AreEqual(expectedModels.ElementAt(i).Email, actualModels.ElementAt(i).Email);
                Assert.AreEqual(expectedModels.ElementAt(i).Address, actualModels.ElementAt(i).Address);
            }
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
            IEnumerable<UserModelOutForAdmins> userModelOuts = new List<UserModelOutForAdmins>
            {
                new UserModelOutForAdmins(user1),
                new UserModelOutForAdmins(user2),
                new UserModelOutForAdmins(user3)
            };


            UserMock.Setup(m => m.GetAll()).Returns(users);


            IActionResult actual = UserController.GetAll();
            IActionResult expected = new OkObjectResult(userModelOuts);
            Assert.AreEqual(expected.GetType(), actual.GetType());

            OkObjectResult actualOk = actual as OkObjectResult;
            IEnumerable<UserModelOutForAdmins> actualModels = actualOk.Value as IEnumerable<UserModelOutForAdmins>;
            OkObjectResult expectedOk = expected as OkObjectResult;
            IEnumerable<UserModelOutForAdmins> expectedModels = expectedOk.Value as IEnumerable<UserModelOutForAdmins>;
            for (int i = 0; i < userModelOuts.Count(); i++)
            {
                Assert.AreEqual(expectedModels.ElementAt(i).Email, actualModels.ElementAt(i).Email);
                Assert.AreEqual(expectedModels.ElementAt(i).Address, actualModels.ElementAt(i).Address);
            }
        }

        [TestMethod]
        public void GetOk()
        {
            User user1 = new User()
            {
                Email = "user@test.com",
                Address = "test 123"
            };
            IEnumerable<User> users = new List<User> { user1 };
            IEnumerable<UserModelOutForCustomers> userModelOuts = new List<UserModelOutForCustomers> { new UserModelOutForCustomers(user1) };
            UserMock.Setup(m => m.Get("user@test.com")).Returns(user1);


            IActionResult actual = UserController.Get("user@test.com");
            IActionResult expected = new OkObjectResult(new UserModelOutForCustomers(user1));
            Assert.AreEqual(expected.GetType(), actual.GetType());

            OkObjectResult actualOk = actual as OkObjectResult;
            UserModelOutForCustomers actualModel = actualOk.Value as UserModelOutForCustomers;
            OkObjectResult expectedOk = expected as OkObjectResult;
            UserModelOutForCustomers expectedModel = expectedOk.Value as UserModelOutForCustomers;
            Assert.AreEqual(expectedModel.Email, actualModel.Email);
            Assert.AreEqual(expectedModel.Address, actualModel.Address);
        }

        [TestMethod]
        public void PostOk()
        {
            UserModelInForCustomers userModel = new UserModelInForCustomers()
            {
                Email = "user@test.com",
                Address = "test 123"
            };
            UserMock.Setup(m => m.Add(It.IsAny<User>()));

            IActionResult actual = UserController.Post(userModel);
            IActionResult expected = new CreatedResult(userModel.Email, "User created");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            CreatedResult actualOk = actual as CreatedResult;
            CreatedResult expectedOk = expected as CreatedResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
        }

        [TestMethod]
        public void PutOk()
        {
            User current = new User()
            {
                Email = "user@test.com",
                Address = "user address"
            };
            UserModelInForCustomers userModel = new UserModelInForCustomers()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            UserMock.Setup(m => m.Update(It.IsAny<User>()));

            IActionResult actual = UserController.Put(current.Email, userModel);
            IActionResult expected = new OkObjectResult("User modified");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
        }

        [TestMethod]
        public void DeleteOk()
        {
            UserModelInForCustomers userModel = new UserModelInForCustomers()
            {
                Email = "test@test.com",
                Address = "test 123"
            };
            UserMock.Setup(m => m.Delete("test@test.com"));

            IActionResult actual = UserController.Delete(userModel.Email);
            IActionResult expected = new OkObjectResult("User deleted");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
        }
    }
}