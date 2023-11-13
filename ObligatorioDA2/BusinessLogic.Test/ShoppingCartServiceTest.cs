using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;
using Moq;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ShoppingCartServiceTest
    {
        Mock<IShoppingCartServiceDatabaseHelper> DatabaseHelperMock { get; set; }
        Mock<IShoppingCartServiceReflectionHelper> ReflectionHelperMock { get; set; }
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

            DatabaseHelperMock.Setup(m => m.GetPromotions()).Returns(promotions);
            ReflectionHelperMock.Setup(m => m.GetPromotions()).Returns(new List<PromotionAbstract>());

            IEnumerable<PromotionAbstract> actual = ShoppingCartService.GetPromotions();
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

            DatabaseHelperMock.Setup(m => m.GetPromotions()).Returns(promotions);
            ReflectionHelperMock.Setup(m=>m.GetPromotions()).Returns(new List<PromotionAbstract>());

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

            DatabaseHelperMock.Setup(m => m.GetPromotions()).Returns(promotions);
            ReflectionHelperMock.Setup(m => m.GetPromotions()).Returns(new List<PromotionAbstract>());

            IEnumerable<PromotionAbstract> actual = ShoppingCartService.GetPromotions();
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(actual.ElementAt(i).GetType(), promotions.ElementAt(i).GetType());
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

            DatabaseHelperMock.Setup(m => m.GetPromotions()).Returns(promotions);
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
    }
}
