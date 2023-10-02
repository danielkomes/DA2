using Domain;
using IBusinessLogic;
using IDataAccess;
using Moq;

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
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p)).Returns(true);
            bool actual = helper.VerifyProduct(p);
            Assert.IsTrue(actual);
            productMock.VerifyAll();
        }

        [TestMethod]
        public void VerifyProductInvalid()
        {
            Product p = new Product();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p)).Returns(false);
            bool actual = helper.VerifyProduct(p);
            Assert.IsFalse(actual);
            productMock.VerifyAll();
        }

        [TestMethod]
        public void VerifyUserValid()
        {
            User u = new User();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            userMock.Setup(m => m.Exists(u)).Returns(true);
            bool actual = helper.VerifyUser(u);
            Assert.IsTrue(actual);
            userMock.VerifyAll();
        }

        [TestMethod]
        public void VerifyUserInvalid()
        {
            User u = new User();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            userMock.Setup(m => m.Exists(u)).Returns(false);
            bool actual = helper.VerifyUser(u);
            Assert.IsFalse(actual);
            userMock.VerifyAll();
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
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p1)).Returns(true);
            productMock.Setup(m => m.Exists(p2)).Returns(true);

            bool actual = helper.VerifyProducts(products);
            Assert.IsTrue(actual);
            productMock.VerifyAll();
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
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(p1)).Returns(true);
            productMock.Setup(m => m.Exists(p2)).Returns(false);

            bool actual = helper.VerifyProducts(products);
            Assert.IsFalse(actual);
            productMock.VerifyAll();
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
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Exists(It.IsAny<Product>())).Returns(false);

            bool actual = helper.VerifyProducts(products);
            Assert.IsFalse(actual);
            productMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void VerifyProductsEmptyProducts()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            IEnumerable<Product> products = new List<Product>();
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
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
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.GetAll()).Returns(promotionEntities);

            IEnumerable<PromotionAbstract> actual = helper.GetPromotions();
            Assert.IsFalse(actual.Any());
            promotionMock.VerifyAll();
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
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.GetAll()).Returns(promotionEntities);

            IEnumerable<PromotionAbstract> actual = helper.GetPromotions();
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).GetType(), promotions.ElementAt(i).GetType());
            }
            promotionMock.VerifyAll();
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
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.GetAll()).Returns(promotionEntities);

            IEnumerable<PromotionAbstract> actual = helper.GetPromotions();
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).GetType(), promotions.ElementAt(i).GetType());
            }
            promotionMock.VerifyAll();
        }

        [TestMethod]
        public void GetPromotions3Promotions()
        {
            PromotionEntity p1 = new PromotionEntity()
            {
                Type = EPromotionType.Promotion20Off
            };
            PromotionEntity p2 = new PromotionEntity()
            {
                Type = EPromotionType.Promotion3x2
            };
            PromotionEntity p3 = new PromotionEntity()
            {
                Type = EPromotionType.PromotionTotalLook
            };
            PromotionAbstract promo1 = new Promotion20Off(p1);
            PromotionAbstract promo2 = new Promotion3x2(p2);
            PromotionAbstract promo3 = new PromotionTotalLook(p3);
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>
            {
                p1, p2, p3
            };
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>
            {
                promo1, promo2, promo3
            };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            promotionMock.Setup(m => m.GetAll()).Returns(promotionEntities);

            IEnumerable<PromotionAbstract> actual = helper.GetPromotions();
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).GetType(), promotions.ElementAt(i).GetType());
            }
            promotionMock.VerifyAll();
        }

        [TestMethod]
        public void InsertPurchaseValid()
        {
            User user = new User();
            IEnumerable<Product> products = new List<Product>();
            Purchase purchase = new Purchase(user, products);
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            purchaseMock.Setup(m => m.Add(purchase));

            helper.InsertPurchase(purchase);
            purchaseMock.VerifyAll();
        }

        [TestMethod]
        public void GetProducts()
        {
            Product p1 = new Product()
            {
                Name = "product1",
                Description = "description1",
                Price = 100
            };
            IEnumerable<Guid> productIds = new List<Guid>() { p1.Id };
            IEnumerable<Product> products = new List<Product>() { p1 };
            var userMock = new Mock<IService<User>>(MockBehavior.Strict);
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                userMock.Object, productMock.Object, promotionMock.Object, purchaseMock.Object);
            productMock.Setup(m => m.Get(It.IsAny<Product>())).Returns(p1);

            IEnumerable<Product> actual = helper.GetProducts(productIds);
            IEnumerable<Product> expected = products;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected.ElementAt(i).Description, actual.ElementAt(i).Description);
                Assert.AreEqual(expected.ElementAt(i).Price, actual.ElementAt(i).Price);
            }
            productMock.VerifyAll();
        }
    }
}
