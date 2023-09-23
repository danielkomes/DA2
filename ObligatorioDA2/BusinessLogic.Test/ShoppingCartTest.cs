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
            Product p = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.VerifyProduct(p)).Returns(true);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p);
            Product expected = p;
            Product actual = cart.ProductsChecked.First();
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void AddToCartInvalid()
        {
            Product p = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.VerifyProduct(p)).Returns(false);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p);

            Assert.IsTrue(!cart.ProductsChecked.Any());
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPriceNoProductsNoPromotions()
        {
            Product p = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionEntity>());

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            float actual = cart.GetTotalPrice();
            float expected = 0;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

    }
}
