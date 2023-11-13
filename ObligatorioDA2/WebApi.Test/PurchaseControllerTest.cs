using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Promotions;
using WebApi.Controllers;
using WebApi.Models.In;

namespace WebApi.Test
{
    [TestClass]
    public class PurchaseControllerTest
    {
        private Mock<IPurchaseLogic> PurchaseMock { get; set; }
        private Mock<IShoppingCart> CartMock { get; set; }
        private PurchaseController PurchaseController { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            PurchaseMock = new Mock<IPurchaseLogic>(MockBehavior.Strict);
            CartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            PurchaseController = new PurchaseController(PurchaseMock.Object, CartMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PurchaseMock.VerifyAll();
        }

        [TestMethod]
        public void GetPurchasesOk()
        {
            Purchase purchase = new Purchase()
            {

            };
            IEnumerable<Purchase> purchases = new List<Purchase>() { purchase };
            PurchaseMock.Setup(m => m.GetAll()).Returns(purchases);

            IActionResult actual = PurchaseController.GetAll();
            IActionResult expected = new OkObjectResult(purchases);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
            PurchaseMock.VerifyAll();
        }

        [TestMethod]
        public void DoPurchaseOk()
        {
            Product p1 = new Product()
            {
                Price = 100
            };
            Product p2 = new Product()
            {
                Price = 200
            };
            Product p3 = new Product()
            {
                Price = 300
            };
            PromotionEntity entity = new PromotionEntity()
            {
                Name = "Promotion 20% off",
                Type = EPromotionType.Promotion20Off
            };
            PromotionAbstract promo20 = new Promotion20Off(entity);
            IEnumerable<Product> currentProducts = new List<Product> { p1, p2, p3 };
            IEnumerable<Guid> currentProductsIds = new List<Guid> { p1.Id, p2.Id, p3.Id };
            EPaymentMethodType paymentMethod = EPaymentMethodType.MasterCard;
            PaymentMethodModelIn paymentMethodModelIn = new PaymentMethodModelIn()
            {
                Type = paymentMethod
            };
            ShoppingCartModelIn model = new ShoppingCartModelIn()
            {
                Products = currentProductsIds,
                PaymentMethod = paymentMethodModelIn
            };
            CartMock.Setup(m => m.GetCurrentProducts(currentProductsIds)).Returns(currentProducts);
            CartMock.Setup(m => m.DoPurchase());
            CartMock.Setup(m => m.GetTotalPrice()).Returns(100 + 200 - 200 * 0.2f);
            CartMock.SetupGet(m => m.PromotionApplied).Returns(promo20);
            CartMock.SetupGet(m => m.ProductsChecked).Returns(currentProducts);
            CartMock.SetupSet(m => m.PaymentMethod);

            var ret = new
            {
                result = "Purchase done",
                promotionApplied = entity.Name,
                totalPrice = 100 + 200 - 200 * 0.2f,
                currentProducts = currentProductsIds
            };
            IActionResult actual = PurchaseController.DoPurchase(model);
            IActionResult expected = new OkResult();

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
    }
}
