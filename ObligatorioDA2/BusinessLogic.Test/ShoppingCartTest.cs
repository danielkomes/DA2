using DataAccess;
using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ShoppingCartTest
    {

        [TestMethod]
        public void AddToCartCorrect()
        {
            //User user = new User();
            //var mock = new Mock<IService<User>>(MockBehavior.Strict);
            //var userService = new UserService(mock.Object);
            //mock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(user);
            //IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper();
        }
    }
}
