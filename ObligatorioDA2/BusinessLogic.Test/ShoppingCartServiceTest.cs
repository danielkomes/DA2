using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;
using Domain.PaymentMethods.CreditCards;
using Domain.PaymentMethods.DebitCards;
using IBusinessLogic;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ShoppingCartServiceTest
    {
        private Mock<IShoppingCartServiceDatabaseHelper> DatabaseHelperMock { get; set; }
        private Mock<IShoppingCartServiceReflectionHelper> ReflectionHelperMock { get; set; }
        private IShoppingCartService ShoppingCartService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseHelperMock = new Mock<IShoppingCartServiceDatabaseHelper>(MockBehavior.Strict);
            ReflectionHelperMock = new Mock<IShoppingCartServiceReflectionHelper>(MockBehavior.Strict);
            ShoppingCartService = new ShoppingCartService(DatabaseHelperMock.Object, ReflectionHelperMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DatabaseHelperMock.VerifyAll();
            ReflectionHelperMock.VerifyAll();
        }
        [TestMethod]
        public void GetPromotionsEmpty()
        {
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>();
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>();

            ReflectionHelperMock.Setup(m => m.GetPromotions()).Returns(new List<PromotionAbstract>());

            IEnumerable<PromotionAbstract> actual = ShoppingCartService.GetPromotions();
            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public void GetPromotions1Promotion()
        {
            PromotionEntity p1 = new PromotionEntity();
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>
            {
                p1
            };
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>
            {
            };

            ReflectionHelperMock.Setup(m => m.GetPromotions()).Returns(new List<PromotionAbstract>());

            IEnumerable<PromotionAbstract> actual = ShoppingCartService.GetPromotions();
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
                Type = EPromotionType.PromotionImported
            };
            PromotionAbstract promo2 = new PromotionImported(null, p2, null, null);
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>
            {
                p1, p2
            };
            IEnumerable<PromotionAbstract> promotionsReflection = new List<PromotionAbstract> { promo2 };

            ReflectionHelperMock.Setup(m => m.GetPromotions()).Returns(promotionsReflection);

            IEnumerable<PromotionAbstract> actual = ShoppingCartService.GetPromotions();
            IEnumerable<PromotionAbstract> expected = new List<PromotionAbstract>() { promo2 };

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).GetType(), actual.ElementAt(i).GetType());
            }
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
            IEnumerable<PromotionEntity> promotionEntities = new List<PromotionEntity>
            {
                p1, p2, p3
            };
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>
            {
            };

            ReflectionHelperMock.Setup(m => m.GetPromotions()).Returns(new List<PromotionAbstract>());

            IEnumerable<PromotionAbstract> actual = ShoppingCartService.GetPromotions();
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).GetType(), promotions.ElementAt(i).GetType());
            }
        }

        [TestMethod]
        public void InsertPurchaseValid()
        {
            User user = new User();
            IEnumerable<Product> products = new List<Product>();
            float total = 0;
            PaymentMethodEntity paymentMethodEntity = new PaymentMethodEntity(user, EPaymentMethodType.MasterCard);
            Purchase purchase = new Purchase(user, products, paymentMethodEntity, total);

            DatabaseHelperMock.Setup(m => m.InsertPurchase(purchase));

            ShoppingCartService.InsertPurchase(purchase);
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

            DatabaseHelperMock.Setup(m => m.GetProducts(productIds)).Returns(products);

            IEnumerable<Product> actual = ShoppingCartService.GetProducts(productIds);
            IEnumerable<Product> expected = products;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected.ElementAt(i).Description, actual.ElementAt(i).Description);
                Assert.AreEqual(expected.ElementAt(i).Price, actual.ElementAt(i).Price);
            }
        }

        [TestMethod]
        public void GetPaymentMethodVisa()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.Visa;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);

            DatabaseHelperMock.Setup(m => m.GetPaymentMethod(entity)).Returns(entity);

            PaymentMethod actual = ShoppingCartService.GetPaymentMethod(user, type);
            PaymentMethod expected = new Visa(entity);

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        [TestMethod]
        public void GetPaymentMethodMasterCard()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.MasterCard;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);

            DatabaseHelperMock.Setup(m => m.GetPaymentMethod(entity)).Returns(entity);

            PaymentMethod actual = ShoppingCartService.GetPaymentMethod(user, type);
            PaymentMethod expected = new MasterCard(entity);

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        [TestMethod]
        public void GetPaymentMethodBbva()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.Bbva;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);

            DatabaseHelperMock.Setup(m => m.GetPaymentMethod(entity)).Returns(entity);

            PaymentMethod actual = ShoppingCartService.GetPaymentMethod(user, type);
            PaymentMethod expected = new Bbva(entity);

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        [TestMethod]
        public void GetPaymentMethodItau()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.Itau;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);

            DatabaseHelperMock.Setup(m => m.GetPaymentMethod(entity)).Returns(entity);

            PaymentMethod actual = ShoppingCartService.GetPaymentMethod(user, type);
            PaymentMethod expected = new Itau(entity);

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        [TestMethod]
        public void GetPaymentMethodSantander()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.Santander;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);

            DatabaseHelperMock.Setup(m => m.GetPaymentMethod(entity)).Returns(entity);

            PaymentMethod actual = ShoppingCartService.GetPaymentMethod(user, type);
            PaymentMethod expected = new Santander(entity);

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        [TestMethod]
        public void GetPaymentMethodPaganza()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.Paganza;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);

            DatabaseHelperMock.Setup(m => m.GetPaymentMethod(entity)).Returns(entity);

            PaymentMethod actual = ShoppingCartService.GetPaymentMethod(user, type);
            PaymentMethod expected = new Paganza(entity);

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        [TestMethod]
        public void GetPaymentMethodPaypal()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.Paypal;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);

            DatabaseHelperMock.Setup(m => m.GetPaymentMethod(entity)).Returns(entity);

            PaymentMethod actual = ShoppingCartService.GetPaymentMethod(user, type);
            PaymentMethod expected = new Paypal(entity);

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
    }
}
