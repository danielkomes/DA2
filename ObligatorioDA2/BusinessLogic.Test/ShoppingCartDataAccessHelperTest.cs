using Domain;
using IBusinessLogic;
using IDataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ShoppingCartDataAccessHelperTest
    {
        [TestMethod]
        public void VerifyProductValid()
        {
            Product p = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p.Id)).Returns(true);
            bool actual = helper.VerifyProduct(p);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void VerifyProductInvalid()
        {
            Product p = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p.Id)).Returns(false);
            bool actual = helper.VerifyProduct(p);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void VerifyUserValid()
        {
            User u = new User();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            userMock.Setup(m => m.Exists(u.Id)).Returns(true);
            bool actual = helper.VerifyUser(u);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void VerifyUserInvalid()
        {
            User u = new User();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            userMock.Setup(m => m.Exists(u.Id)).Returns(false);
            bool actual = helper.VerifyUser(u);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void VerifyPromotionValid()
        {
            PromotionEntity p = new PromotionEntity();
            PromotionAbstract promo = new Promotion20Off(p);
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.Exists(promo.PromotionEntity.Id)).Returns(true);
            bool actual = helper.VerifyPromotion(promo.PromotionEntity);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void VerifyPromotionInvalid()
        {
            PromotionEntity p = new PromotionEntity();
            PromotionAbstract promo = new Promotion20Off(p);
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.Exists(promo.PromotionEntity.Id)).Returns(false);
            bool actual = helper.VerifyPromotion(promo.PromotionEntity);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void VerifyProductsValid()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            IEnumerable<Product> products = new List<Product> { p1, p2 };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p1.Id)).Returns(true);
            productMock.Setup(m => m.Exists(p2.Id)).Returns(true);

            bool actual = helper.VerifyProducts(products);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void VerifyProducts1Invalid()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            IEnumerable<Product> products = new List<Product> { p1, p2 };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p1.Id)).Returns(true);
            productMock.Setup(m => m.Exists(p2.Id)).Returns(false);

            bool actual = helper.VerifyProducts(products);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void VerifyProductsAllInvalid()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            IEnumerable<Product> products = new List<Product> { p1, p2 };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p1.Id)).Returns(false);
            productMock.Setup(m => m.Exists(p2.Id)).Returns(false);

            bool actual = helper.VerifyProducts(products);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void VerifyProductsEmptyProducts()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            IEnumerable<Product> products = new List<Product>();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);

            bool actual = helper.VerifyProducts(products);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GetPromotionsEmpty()
        {
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>();
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.GetAll()).Returns(promotionEntities);

            IEnumerable<PromotionAbstract> actual = helper.GetPromotions();
            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public void GetPromotions1Promotion()
        {
            PromotionEntity p1 = new PromotionEntity();
            PromotionAbstract promo1 = new Promotion20Off(p1);
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>
            {
                p1
            };
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>
            {
                promo1
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.GetAll()).Returns(promotionEntities);

            IEnumerable<PromotionAbstract> actual = helper.GetPromotions();
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).GetType(), promotions.ElementAt(i).GetType());
            }
        }

        [TestMethod]
        public void GetPromotions2Promotions()
        {
            PromotionEntity p1 = new PromotionEntity()
            {
                Type = EPromotionType.Promotion20Off
            };
            PromotionEntity p2 = new PromotionEntity()
            {
                Type = EPromotionType.Promotion3x2
            };
            PromotionAbstract promo1 = new Promotion20Off(p1);
            PromotionAbstract promo2 = new Promotion3x2(p2);
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>
            {
                p1, p2
            };
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>
            {
                promo1, promo2
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.GetAll()).Returns(promotionEntities);

            IEnumerable<PromotionAbstract> actual = helper.GetPromotions();
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).GetType(), promotions.ElementAt(i).GetType());
            }
        }
    }
}
