using BusinessLogic.Exceptions;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class SignupLogicTest
    {
        private Mock<IService<User>> UserMock { get; set; }
        private SignupLogic SignupLogic { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UserMock = new Mock<IService<User>>(MockBehavior.Strict);
            SignupLogic = new SignupLogic(UserMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            UserMock.VerifyAll();
        }

        [TestMethod]
        public void SignupOk()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            UserMock.Setup(m => m.Add(user));

            SignupLogic.Signup(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistsException))]
        public void SignupEmailAlreadyExists()
        {
            User user = new User()
            {
                Email = "user@test.com"
            };
            UserMock.Setup(m => m.Exists(user)).Returns(true);

            SignupLogic.Signup(user);
        }
    }
}
