﻿using DataAccess;
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
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
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
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
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
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
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
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
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
            Product p1 = new Product()
            {
                Price = 100
            };
            Product p2 = new Product()
            {
                Price = 200
            };
            PromotionEntity promotionEntity = new PromotionEntity();
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off
            };

            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.VerifyProduct(It.IsAny<Product>())).Returns(true);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1);
            cart.AddToCart(p2);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 - 200 * 0.2f;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice2Product2Promotion()
        {
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat1"
            };
            PromotionEntity promotionEntity = new PromotionEntity();
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            Promotion3x2 promotion3x2 = new Promotion3x2(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off, promotion3x2
            };

            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.VerifyProduct(It.IsAny<Product>())).Returns(true);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1);
            cart.AddToCart(p2);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 - 200 * 0.2f;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice3Product2Promotion()
        {
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1"
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat1"
            };
            Product p3 = new Product()
            {
                Price = 300,
                Category = "cat1"
            };
            PromotionEntity promotionEntity = new PromotionEntity();
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            Promotion3x2 promotion3x2 = new Promotion3x2(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off, promotion3x2
            };

            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.VerifyProduct(It.IsAny<Product>())).Returns(true);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1);
            cart.AddToCart(p2);
            cart.AddToCart(p3);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 + 300 - 100;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice3Product3Promotion()
        {
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1",
                Colors = new List<string> { "red", "blue" }
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat1",
                Colors = new List<string> { "red", "green" }
            };
            Product p3 = new Product()
            {
                Price = 300,
                Category = "cat1",
                Colors = new List<string> { "red", "yellow" }
            };
            PromotionEntity promotionEntity = new PromotionEntity();
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            Promotion3x2 promotion3x2 = new Promotion3x2(promotionEntity);
            PromotionTotalLook promotionTotalLook = new PromotionTotalLook(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off, promotion3x2, promotionTotalLook
            };

            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.VerifyProduct(It.IsAny<Product>())).Returns(true);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1);
            cart.AddToCart(p2);
            cart.AddToCart(p3);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 + 300 - 300 * 0.5f;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void RemoveFromCartEmpty()
        {
            Product p1 = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.RemoveFromCart(p1);
            List<Product> actual = cart.ProductsChecked.ToList();
            Assert.IsTrue(!actual.Any());
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void RemoveFromCart1Product()
        {
            Product p1 = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.VerifyProduct(It.IsAny<Product>())).Returns(true);
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1);
            cart.RemoveFromCart(p1);
            List<Product> actual = cart.ProductsChecked.ToList();
            Assert.IsTrue(!actual.Any());
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void RemoveFromCart1ProductNotAdded()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.VerifyProduct(It.IsAny<Product>())).Returns(true);
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1);
            cart.RemoveFromCart(p2);
            List<Product> actual = cart.ProductsChecked.ToList();
            Assert.AreEqual(1, actual.Count());
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void DoPurchase1Product()
        {
            User user = new User();
            Product p1 = new Product();
            List<Product> products = new List<Product> { p1 };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(h => h.VerifyUser(user)).Returns(true);
            helperMock.Setup(h => h.VerifyProduct(p1)).Returns(true);
            helperMock.Setup(h => h.VerifyProducts(products)).Returns(true);

            //Purchase purchase = new Purchase(user, products);
            helperMock.Setup(h => h.InsertPurchase(It.IsAny<Purchase>()));
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1);
            cart.User = user;
            cart.DoPurchase();
        }
    }
}