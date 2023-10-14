using Domain;
using IBusinessLogic;
using IDataAccess;
using Moq;
using System.Security.Authentication;

namespace BusinessLogic.Test
{
    [TestClass]
    public class SessionLogicTest
    {
        [TestMethod]
        public void AuthenticateOk()
        {
            User user = new User()
            {
                Email = "user@test.com",
                Password = "pass1"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<IService<Session>>(MockBehavior.Strict);
            ISessionLogic sessionLogic = new SessionLogic(sessionMock.Object, userMock.Object);
            userMock.Setup(m => m.Get(user)).Returns(user);
            sessionMock.Setup(m => m.Add(It.IsAny<Session>()));

            Guid actual = sessionLogic.Authenticate(user);

            sessionMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void AuthenticateFailUserNotExists()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<IService<Session>>(MockBehavior.Strict);
            ISessionLogic sessionLogic = new SessionLogic(sessionMock.Object, userMock.Object);
            userMock.Setup(m => m.Get(user)).Returns(value: null as User);
            sessionLogic.Authenticate(user);
            sessionMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void AuthenticateFailIncorrectPassword()
        {
            User user = new User()
            {
                Email = "user@test.com",
                Password = "pass1"
            };
            User userIncorrect = new User()
            {
                Email = "user@test.com",
                Password = "incorrectPassword"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<IService<Session>>(MockBehavior.Strict);
            ISessionLogic sessionLogic = new SessionLogic(sessionMock.Object, userMock.Object);
            userMock.Setup(m => m.Get(userIncorrect)).Returns(user);

            sessionLogic.Authenticate(userIncorrect);

            sessionMock.VerifyAll();
        }

        [TestMethod]
        public void GetCurrentUserOk()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            Session session = new Session()
            {
                User = user
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<IService<Session>>(MockBehavior.Strict);
            ISessionLogic sessionLogic = new SessionLogic(sessionMock.Object, userMock.Object);
            sessionMock.Setup(m => m.Get(It.IsAny<Session>())).Returns(session);
            User? actual = sessionLogic.GetCurrentUser(session.Id);
            User expected = user;
            Assert.AreEqual(expected, actual);
            sessionMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCurrentUserNoToken()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            Session session = new Session()
            {
                User = user
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<IService<Session>>(MockBehavior.Strict);
            ISessionLogic sessionLogic = new SessionLogic(sessionMock.Object, userMock.Object);
            sessionMock.Setup(m => m.Get(It.IsAny<Session>())).Returns(session);
            User? actual = sessionLogic.GetCurrentUser(null);
        }

        [TestMethod]
        public void LogoutOk()
        {
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<IService<Session>>(MockBehavior.Strict);
            ISessionLogic sessionLogic = new SessionLogic(sessionMock.Object, userMock.Object);
            sessionMock.Setup(m => m.Get(It.IsAny<Session>())).Returns(new Session());
            sessionMock.Setup(m => m.Delete(It.IsAny<Session>()));
            sessionLogic.Logout();

            User actual = sessionLogic.GetCurrentUser(new Session().Id);
            Assert.IsNull(actual);
            sessionMock.VerifyAll();
        }
    }
}
