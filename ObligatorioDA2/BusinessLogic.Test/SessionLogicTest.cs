using Domain;
using IBusinessLogic;
using IDataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

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
                Email = "user@test.com"
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var sessionMock = new Mock<IService<Session>>(MockBehavior.Strict);
            ISessionLogic sessionLogic = new SessionLogic(sessionMock.Object, userMock.Object);
            userMock.Setup(m => m.Get(user)).Returns(user);
            sessionMock.Setup(m => m.Add(It.IsAny<Session>()));
            sessionMock.Setup(m => m.Save());

            Guid actual = sessionLogic.Authenticate(user);
            Guid expected = user.Id;

            Assert.AreEqual(expected.GetType(), actual.GetType());
            sessionMock.VerifyAll();
            userMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void AuthenticateFail()
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
            userMock.VerifyAll();
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
            userMock.VerifyAll();
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
    }
}
