using Domain;
using IBusinessLogic;
using IDataAccess;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class UserLogicTest
    {

        [TestMethod]
        public void AddValid()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserLogic logic = new UserLogic(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Add(user));
            sessionMock.Setup(m => m.GetCurrentUser(null)).Returns(user);

            logic.Add(user);

            userMock.VerifyAll();
            sessionMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteValid()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserLogic logic = new UserLogic(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Delete(It.IsAny<User>()));

            logic.Delete(user.Email);

            userMock.VerifyAll();
        }

        [TestMethod]
        public void GetValid()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserLogic logic = new UserLogic(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Get(It.IsAny<User>())).Returns(user);

            User actual = logic.Get(user.Email);

            Assert.AreEqual(user, actual);
            userMock.VerifyAll();
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
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserLogic logic = new UserLogic(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.GetAll()).Returns(users);

            IEnumerable<User> actual = logic.GetAll();

            for (int i = 0; i < users.Count(); i++)
            {
                Assert.AreEqual(users.ElementAt(i).Id, actual.ElementAt(i).Id);
                Assert.AreEqual(users.ElementAt(i).Email, actual.ElementAt(i).Email);
                Assert.AreEqual(users.ElementAt(i).Address, actual.ElementAt(i).Address);
            }
            userMock.VerifyAll();
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
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            UserLogic logic = new UserLogic(userMock.Object, sessionMock.Object);
            userMock.Setup(m => m.Update(userCurrent));

            logic.Update(userUpdated);

            userMock.VerifyAll();
        }
    }
}