using BusinessLogic.Exceptions;
using Domain;
using IBusinessLogic;
using IDataAccess;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class UserLogicTest
    {
        private Mock<IService<User>> UserMock { get; set; }
        private Mock<ISessionLogic> SessionMock { get; set; }
        private UserLogic UserLogic { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UserMock = new Mock<IService<User>>(MockBehavior.Strict);
            SessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserLogic = new UserLogic(UserMock.Object, SessionMock.Object);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            UserMock.VerifyAll();
            SessionMock.VerifyAll();
        }

        [TestMethod]
        public void AddValid()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            UserMock.Setup(m => m.Exists(user)).Returns(false);
            UserMock.Setup(m => m.Add(user));

            UserLogic.Add(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistsException))]
        public void AddEmailAlreadyExists()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            UserMock.Setup(m => m.Exists(user)).Returns(true);

            UserLogic.Add(user);
        }

        [TestMethod]
        public void DeleteValid()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            UserMock.Setup(m => m.Delete(It.IsAny<User>()));

            UserLogic.Delete(user.Email);
        }

        [TestMethod]
        public void GetValid()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            UserMock.Setup(m => m.Get(It.IsAny<User>())).Returns(user);
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(user);

            User actual = UserLogic.Get(user.Email);

            Assert.AreEqual(user, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ProfileMismatchException))]
        public void GetAnotherBeingCustomer()
        {
            User userTarget = new User()
            {
                Email = "userTarget@test.com"
            };
            User userCurrent = new User()
            {
                Email = "userCurrent@test.com"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);

            User actual = UserLogic.Get(userTarget.Email);
        }

        [TestMethod]
        public void GetAnotherBeingAdmin()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            User admin = new User()
            {
                Email = "admin@admin.com",
                Roles = new List<EUserRole>() { EUserRole.Admin }
            };
            UserMock.Setup(m => m.Get(It.IsAny<User>())).Returns(user);
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(admin);

            User actual = UserLogic.Get(user.Email);

            Assert.AreEqual(user, actual);
        }

        [TestMethod]
        public void GetAllValid()
        {
            User user1 = new User()
            {
                Email = "user@test.com"
            };
            User user2 = new User()
            {
                Email = "user@test.com"
            };
            IEnumerable<User> users = new List<User>() { user1, user2 };
            UserMock.Setup(m => m.GetAll()).Returns(users);

            IEnumerable<User> actual = UserLogic.GetAll();

            for (int i = 0; i < users.Count(); i++)
            {
                Assert.AreEqual(users.ElementAt(i).Id, actual.ElementAt(i).Id);
                Assert.AreEqual(users.ElementAt(i).Email, actual.ElementAt(i).Email);
                Assert.AreEqual(users.ElementAt(i).Address, actual.ElementAt(i).Address);
            }
        }

        [TestMethod]
        public void UpdateValid()
        {
            User userCurrent = new User()
            {
                Email = "user@test.com"
            };
            User userUpdated = new User()
            {
                Id = userCurrent.Id,
                Email = "updatedUser@test.com"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);
            UserMock.Setup(m => m.Exists(userUpdated)).Returns(false);
            UserMock.Setup(m => m.Update(userUpdated));

            UserLogic.Update(userCurrent.Email, userUpdated);
        }

        [TestMethod]
        public void UpdateSameEmailValid()
        {
            User userCurrent = new User()
            {
                Email = "user@test.com",
                Address = "123"
            };
            User userUpdated = new User()
            {
                Id = userCurrent.Id,
                Email = "user@test.com",
                Address = "456"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);
            UserMock.Setup(m => m.Exists(userUpdated)).Returns(true);
            UserMock.Setup(m => m.Get(userUpdated)).Returns(userCurrent);
            UserMock.Setup(m => m.Update(userUpdated));

            UserLogic.Update(userCurrent.Email, userUpdated);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistsException))]
        public void UpdateEmailAlreadyExists()
        {
            User userCurrent = new User()
            {
                Email = "user@test.com"
            };
            User userUpdated = new User()
            {
                Id = userCurrent.Id,
                Email = "updatedUser@test.com"
            };
            User userAnother = new User()
            {
                Email = "updatedUser@test.com"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);
            UserMock.Setup(m => m.Exists(userUpdated)).Returns(true);
            UserMock.Setup(m => m.Get(userUpdated)).Returns(userAnother);

            UserLogic.Update(userCurrent.Email, userUpdated);
        }

        [TestMethod]
        [ExpectedException(typeof(ProfileMismatchException))]
        public void UpdateBeingAnotherCustomer()
        {
            User userCurrent = new User()
            {
                Email = "currentUser@test.com"
            };
            User userTargetOld = new User()
            {
                Email = "oldUser@test.com"
            };
            User userTargetUpdated = new User()
            {
                Id = userTargetOld.Id,
                Email = "updatedUser@test.com"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);

            UserLogic.Update(userTargetOld.Email, userTargetUpdated);
        }

        [TestMethod]
        [ExpectedException(typeof(ProfileMismatchException))]
        public void UpdateEmailAlreadyExistsBeingAnotherCustomer()
        {
            User userCurrent = new User()
            {
                Email = "currentUser@test.com",
            };
            User userTargetOld = new User()
            {
                Email = "userOld@test.com",
                Address = "123"
            };
            User userTargetUpdated = new User()
            {
                Id = userTargetOld.Id,
                Email = "userUpdated@test.com",
                Address = "456"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);

            UserLogic.Update(userTargetOld.Email, userTargetUpdated);
        }

        [TestMethod]
        public void UpdateBeingAnotherAdmin()
        {
            User userCurrent = new User()
            {
                Email = "currentUser@test.com",
                Roles = new List<EUserRole>() { EUserRole.Admin }
            };
            User userTargetOld = new User()
            {
                Email = "oldUser@test.com"
            };
            User userTargetUpdated = new User()
            {
                Id = userTargetOld.Id,
                Email = "updatedUser@test.com"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);
            UserMock.Setup(m => m.Exists(userTargetUpdated)).Returns(false);
            UserMock.Setup(m => m.Update(userTargetUpdated));

            UserLogic.Update(userTargetOld.Email, userTargetUpdated);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistsException))]
        public void UpdateEmailAlreadyExistsBeingAnotherAdmin()
        {
            User userCurrent = new User()
            {
                Email = "currentUser@test.com",
                Roles = new List<EUserRole>() { EUserRole.Admin }
            };
            User userTargetOld = new User()
            {
                Email = "userOld@test.com",
                Address = "123"
            };
            User userTargetUpdated = new User()
            {
                Id = userTargetOld.Id,
                Email = "userUpdated@test.com",
                Address = "456"
            };
            SessionMock.Setup(m => m.GetCurrentUser(null)).Returns(userCurrent);
            UserMock.Setup(m => m.Exists(userTargetUpdated)).Returns(true);
            UserMock.Setup(m => m.Get(userTargetUpdated)).Returns(userTargetOld);

            UserLogic.Update(userTargetOld.Email, userTargetUpdated);
        }
    }
}