using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Promotions;
using WebApi.Controllers;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Test
{
    [TestClass]
    public class ShoppingCartControllerTest
    {
        private Mock<IShoppingCart> CartMock { get; set; }
        private ShoppingCartController cartController;

        [TestInitialize]
        public void TestInitialize()
        {
            CartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            cartController = new ShoppingCartController(CartMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CartMock.VerifyAll();
        }

        [TestMethod]
        public void GetAll3ProductsCheckedOk()
        {
            Product p1 = new Product()
            {
                Price = 100,
                Category = "cat1",
                Colors = new List<string> { "red" }
            };
            Product p2 = new Product()
            {
                Price = 200,
                Category = "cat2",
                Colors = new List<string> { "green" }
            };
            Product p3 = new Product()
            {
                Price = 300,
                Category = "cat3",
                Colors = new List<string> { "blue" }
            };
            PromotionEntity entity = new PromotionEntity()
            {
                Name = "Promotion 20% off",
                Type = EPromotionType.Promotion20Off
            };
            PromotionAbstract promo20 = new Promotion20Off(entity);
            IEnumerable<Product> currentProducts = new List<Product> { p1, p2, p3 };
            IEnumerable<ProductModelOut> productModels = new List<ProductModelOut>();
            productModels = productModels.Append(new ProductModelOut(p1));
            productModels = productModels.Append(new ProductModelOut(p2));
            productModels = productModels.Append(new ProductModelOut(p3));
            IEnumerable<Guid> currentProductsIds = new List<Guid>
            {
                p1.Id,
                p2.Id,
                p3.Id,
            };
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
            CartMock.Setup(m => m.GetTotalPrice()).Returns(100 + 200 - 200 * 0.2f);
            CartMock.Setup(m => m.ProductsChecked).Returns(currentProducts);
            CartMock.SetupGet(m => m.ProductsChecked).Returns(currentProducts);
            CartMock.SetupGet(m => m.PromotionApplied).Returns(promo20);
            CartMock.SetupSet(m => m.PaymentMethod);


            var ret = new
            {
                checkedProducts = productModels,
                promotionApplied = entity.Name,
                totalPrice = 100 + 200 - 200 * 0.2f,
            };
            IActionResult actual = cartController.CalculateTotal(model);
            IActionResult expected = new OkObjectResult(ret);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
        }

    }
}
