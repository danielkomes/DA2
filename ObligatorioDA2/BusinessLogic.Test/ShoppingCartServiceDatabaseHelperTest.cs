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
    public class ShoppingCartServiceDatabaseHelperTest
    {
        private Mock<IService<Product>> ProductMock;
        private Mock<IService<PromotionEntity>> PromotionMock;
        private Mock<IService<Purchase>> PurchaseMock;
        private Mock<IService<PaymentMethodEntity>> PaymentMethodMock;

        private IShoppingCartServiceDatabaseHelper Helper { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ProductMock = new Mock<IService<Product>>(MockBehavior.Strict);
            PromotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            PurchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            PaymentMethodMock = new Mock<IService<PaymentMethodEntity>>(MockBehavior.Strict);

            Helper = new ShoppingCartServiceDatabaseHelper(ProductMock.Object, PromotionMock.Object, PurchaseMock.Object, PaymentMethodMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ProductMock.VerifyAll();
            PromotionMock.VerifyAll();
            PurchaseMock.VerifyAll();
            PaymentMethodMock.VerifyAll();
        }

        [TestMethod]
        public void GetProductsNoProducts()
        {
            IEnumerable<Guid> productIds = new List<Guid>();
            IEnumerable<Product> products = new List<Product>();

            IEnumerable<Product> actual = Helper.GetProducts(productIds);
            IEnumerable<Product> expected = products;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        [TestMethod]
        public void GetProducts1Product()
        {
            Product p1 = new Product();
            IEnumerable<Guid> productIds = new List<Guid>() { p1.Id };
            IEnumerable<Product> products = new List<Product>() { p1 };

            ProductMock.Setup(m => m.Get(p1)).Returns(p1);

            IEnumerable<Product> actual = Helper.GetProducts(productIds);
            IEnumerable<Product> expected = products;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        [TestMethod]
        public void GetProducts2Products()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            IEnumerable<Guid> productIds = new List<Guid>() { p1.Id, p2.Id };
            IEnumerable<Product> products = new List<Product>() { p1, p2 };

            ProductMock.Setup(m => m.Get(p1)).Returns(p1);
            ProductMock.Setup(m => m.Get(p2)).Returns(p2);

            IEnumerable<Product> actual = Helper.GetProducts(productIds);
            IEnumerable<Product> expected = products;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        [TestMethod]
        public void GetProductOk()
        {
            Product p1 = new Product();

            ProductMock.Setup(m => m.Get(p1)).Returns(p1);

            Product actual = Helper.GetProduct(p1.Id);
            Product expected = p1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPromotionsNoPromotions()
        {
            IEnumerable<PromotionEntity> promotions = new List<PromotionEntity>();
            PromotionMock.Setup(m => m.GetAll()).Returns(promotions);

            IEnumerable<PromotionAbstract> actual = Helper.GetPromotions();
            IEnumerable<PromotionAbstract> expected = new List<PromotionAbstract>();

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).PromotionEntity, actual.ElementAt(i).PromotionEntity);
            }
        }

        [TestMethod]
        public void GetPromotions3Promotions()
        {
            PromotionAbstract p1 = new Promotion20Off(new PromotionEntity() { Type = EPromotionType.Promotion20Off });
            PromotionAbstract p2 = new Promotion3x2(new PromotionEntity() { Type = EPromotionType.Promotion3x2 });
            PromotionAbstract p3 = new PromotionTotalLook(new PromotionEntity() { Type = EPromotionType.PromotionTotalLook });
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract> { p1, p2, p3 };
            IEnumerable<PromotionEntity> entities = new List<PromotionEntity>() { p1.PromotionEntity, p2.PromotionEntity, p3.PromotionEntity };
            PromotionMock.Setup(m => m.GetAll()).Returns(entities);

            IEnumerable<PromotionAbstract> actual = Helper.GetPromotions();
            IEnumerable<PromotionAbstract> expected = promotions;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).PromotionEntity, actual.ElementAt(i).PromotionEntity);
            }
        }

        [TestMethod]
        public void GetPaymentMethodOk()
        {
            User user = new User();
            EPaymentMethodType type = EPaymentMethodType.Visa;
            PaymentMethodEntity entity = new PaymentMethodEntity(user, type);
            PaymentMethod method = new Visa(entity);

            PaymentMethodMock.Setup(m => m.Get(entity)).Returns(entity);

            PaymentMethodEntity actual = Helper.GetPaymentMethod(entity);
            PaymentMethodEntity expected = entity;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertPurchaseOk()
        {
            PaymentMethod method = new Visa(new PaymentMethodEntity(new User(), EPaymentMethodType.Visa));
            Purchase purchase = new Purchase()
            {
                PaymentMethod = method.Entity
            };

            PurchaseMock.Setup(m => m.Add(purchase));
            PaymentMethodMock.Setup(m => m.Exists(purchase.PaymentMethod)).Returns(false);
            PaymentMethodMock.Setup(m => m.Add(purchase.PaymentMethod));

            Helper.InsertPurchase(purchase);
        }
    }
}
