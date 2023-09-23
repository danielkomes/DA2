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
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionAbstract>());

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            float actual = cart.GetTotalPrice();
            float expected = 0;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice1ProductNoPromotions()
        {
            Product p = new Product()
            {
                Price = 100
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionAbstract>());
            helperMock.Setup(sp => sp.VerifyProduct(p)).Returns(true);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p);
            float actual = cart.GetTotalPrice();
            float expected = 100;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice2ProductPromotion20Off()
        {
            Product p = new Product()
            {
                Price = 100
            };
            PromotionEntity promotion = new PromotionEntity()
            {

            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionEntity>() { promotion });
            helperMock.Setup(sp => sp.VerifyProduct(p)).Returns(false);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p);
            float actual = cart.GetTotalPrice();
            float expected = 0;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }
    }
}
