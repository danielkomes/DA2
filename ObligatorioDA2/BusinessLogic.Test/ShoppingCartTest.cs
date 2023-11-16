using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.CreditCards;
using IBusinessLogic;
using Moq;

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
        public void ApplyPaymentMethodDiscountOk()
        {
            float total = 100;
            ShoppingCart.PaymentMethod = EPaymentMethodType.Paganza;

            float actual = ShoppingCart.ApplyPaymentMethodDiscount(total);
            float expected = 100 - 100 * 0.1f;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTotalPrice1ProductPaymentMethodDiscount()
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
            ShoppingCart.PaymentMethod = EPaymentMethodType.Paganza;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100 - 100 * 0.1f;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTotalPrice2Products()
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
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract>
            {
            };
            IEnumerable<Product> products = new List<Product>() { p1, p2 };
            IEnumerable<Guid> productIds = new List<Guid>() { p1.Id, p2.Id };

            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            ShoppingCart.ProductsChecked = products;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100 + 200;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTotalPrice3Products()
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
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract>
            {
            };
            IEnumerable<Product> products = new List<Product>() { p1, p2, p3 };

            ServiceMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            ShoppingCart.ProductsChecked = products;

            float actual = ShoppingCart.GetTotalPrice();
            float expected = 100 + 200 + 300;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void DoPurchaseNoProducts()
        {
            ShoppingCart.DoPurchase();
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

        [TestMethod]
        public void DoPurchase2ProductsPromotionApplied()
        {
            User user = new User();
            Product p1 = new Product();
            EPaymentMethodType paymentMethodType = EPaymentMethodType.Visa;
            List<Product> products = new List<Product> { p1 };
            List<Guid> productIds = new List<Guid> { p1.Id };
            PromotionEntity promotionEntity = new PromotionEntity()
            {
                Type = EPromotionType.Promotion20Off
            };
            ServiceMock.Setup(h => h.GetPromotions()).Returns(new List<PromotionAbstract>());
            ServiceMock.Setup(h => h.GetPaymentMethod(user, paymentMethodType)).Returns(new Visa(new PaymentMethodEntity(user, paymentMethodType)));
            ServiceMock.Setup(h => h.InsertPurchase(It.IsAny<Purchase>()));
            ShoppingCart.ProductsChecked = products;
            ShoppingCart.User = user;
            ShoppingCart.PaymentMethod = paymentMethodType;

            ShoppingCart.DoPurchase();
        }

        [TestMethod]
        public void GetCurrentProductsOk()
        {
            Product p1 = new Product();
            Product p2 = new Product();

            IEnumerable<Guid> productIds = new List<Guid> { p1.Id, p2.Id };
            IEnumerable<Product> products = new List<Product>() { p1, p2 };

            ServiceMock.Setup(m => m.GetProducts(productIds)).Returns(products);

            IEnumerable<Product> actual = ShoppingCart.GetCurrentProducts(productIds);
            IEnumerable<Product> expected = products;

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }
    }
}
