using DataAccess.Exceptions;
using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;
using Domain.PaymentMethods.CreditCards;
using IBusinessLogic;
using IDataAccess;
using Moq;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ShoppingCartTest
    {
        Mock<IShoppingCartService> ServiceMock { get; set; }
        private IShoppingCart ShoppingCart { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ServiceMock = new Mock<IShoppingCartService>(MockBehavior.Strict);
            ShoppingCart = new ShoppingCart(ServiceMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ServiceMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPriceNoProductsNoPromotions()
        {
            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionAbstract>());

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTotalPrice1ProductNoPromotions()
        {
            Product p = new Product()
            {
                Price = 100
            };
            IEnumerable<Product> products = new List<Product>() { p };
            IEnumerable<Guid> productIds = new List<Guid>() { p.Id };

            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionAbstract>());
            ShoppingCart.ProductsChecked = products;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100;
            Assert.AreEqual(expected, actual);
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
            IEnumerable<Product> products = new List<Product>() { p1, p2 };

            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            ShoppingCart.ProductsChecked = products;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100 + 200 - 200 * 0.2f;
            Assert.AreEqual(expected, actual);
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
            IEnumerable<Product> products = new List<Product>() { p1, p2 };
            IEnumerable<Guid> productIds = new List<Guid>() { p1.Id, p2.Id };

            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            ShoppingCart.ProductsChecked = products;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100 + 200 - 200 * 0.2f;
            Assert.AreEqual(expected, actual);
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
            IEnumerable<Product> products = new List<Product>() { p1, p2, p3 };

            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            ShoppingCart.ProductsChecked = products;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100 + 200 + 300 - 100;
            Assert.AreEqual(expected, actual);
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
            IEnumerable<Product> products = new List<Product>() { p1, p2, p3 };

            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            ShoppingCart.ProductsChecked = products;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100 + 200 + 300 - 300 * 0.5f;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DoPurchase1Product()
        {
            User user = new User();
            EPaymentMethodType paymentMethodType = EPaymentMethodType.Visa;
            Product p1 = new Product();
            List<Product> products = new List<Product> { p1 };
            List<Guid> productIds = new List<Guid> { p1.Id };

            ServiceMock.Setup(h => h.GetPromotions()).Returns(new List<PromotionAbstract>());
            ServiceMock.Setup(h => h.GetPaymentMethod(user, paymentMethodType)).Returns(new Visa(new PaymentMethodEntity(user, paymentMethodType)));
            ServiceMock.Setup(h => h.InsertPurchase(It.IsAny<Purchase>()));
            ShoppingCart.ProductsChecked = products;
            ShoppingCart.User = user;
            ShoppingCart.PaymentMethod = paymentMethodType;

            ShoppingCart.DoPurchase();
        }
    }
}
