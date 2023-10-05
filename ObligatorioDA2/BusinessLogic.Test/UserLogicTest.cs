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
    }
}