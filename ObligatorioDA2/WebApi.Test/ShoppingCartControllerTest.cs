﻿using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Promotions;
using WebApi.Controllers;
using WebApi.Models.Out;

namespace WebApi.Test
{
    [TestClass]
    public class ShoppingCartControllerTest
    {
        private Mock<IShoppingCart> cartMock { get; set; }
        private ShoppingCartController cartController;

        [TestInitialize]
        public void TestInitialize()
        {
            cartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            cartController = new ShoppingCartController(cartMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            cartMock.VerifyAll();
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
            cartMock.Setup(m => m.GetCurrentProducts(currentProductsIds)).Returns(currentProducts);
            cartMock.Setup(m => m.GetTotalPrice()).Returns(100 + 200 - 200 * 0.2f);
            cartMock.Setup(m => m.ProductsChecked).Returns(currentProducts);
            cartMock.SetupGet(m => m.ProductsChecked).Returns(currentProducts);
            cartMock.SetupGet(m => m.PromotionApplied).Returns(promo20);


            var ret = new
            {
                checkedProducts = productModels,
                promotionApplied = entity.Name,
                totalPrice = 100 + 200 - 200 * 0.2f,
            };
            IActionResult actual = cartController.GetProducts(currentProductsIds);
            IActionResult expected = new OkObjectResult(ret);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
        }

        [TestMethod]
        public void RemoveProductOk()
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
            IEnumerable<Guid> afterProductsIds = new List<Guid>
            {
                p1.Id,
                p2.Id,
            };
            cartMock.Setup(m => m.GetCurrentProducts(currentProductsIds)).Returns(currentProducts);
            cartMock.Setup(m => m.GetTotalPrice()).Returns(100 + 200 - 200 * 0.2f);
            cartMock.Setup(m => m.RemoveFromCart(It.IsAny<Guid>()));
            cartMock.Setup(m => m.ProductsChecked).Returns(currentProducts);
            cartMock.SetupGet(m => m.ProductsChecked).Returns(currentProducts);
            cartMock.SetupGet(m => m.PromotionApplied).Returns(promo20);


            var ret = new
            {
                result = "Product removed from cart",
                promotionApplied = "Promotion 20% off",
                totalPrice = 100 + 200 - 200 * 0.2f,
                currentProducts = afterProductsIds
            };
            IActionResult actual = cartController.RemoveSelectedProduct(p3.Id, currentProductsIds);
            IActionResult expected = new OkObjectResult(ret);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
        }

        [TestMethod]
        public void RemoveAllProductsOk()
        {
            cartMock.SetupSet(m => m.ProductsChecked = It.IsAny<IEnumerable<Product>>());


            IActionResult actual = cartController.RemoveAllProducts();
            IActionResult expected = new OkObjectResult("All products removed");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
        }

        [TestMethod]
        public void AddProductOk()
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
            IEnumerable<Product> currentProducts = new List<Product> { p1, p2 };
            IEnumerable<Guid> currentProductsIds = new List<Guid> { p1.Id, p2.Id };
            IEnumerable<Guid> afterProducts = new List<Guid> { p1.Id, p2.Id, p3.Id };
            cartMock.Setup(m => m.AddToCart(It.IsAny<Guid>()));
            cartMock.Setup(m => m.GetCurrentProducts(It.IsAny<IEnumerable<Guid>>())).Returns(currentProducts);
            cartMock.SetupGet(m => m.ProductsChecked).Returns(currentProducts);
            cartMock.SetupGet(m => m.PromotionApplied).Returns(promo20);
            cartMock.Setup(m => m.GetTotalPrice()).Returns(100 + 200 - 200 * 0.2f);

            var ret = new
            {
                result = "Product added to cart",
                promotionApplied = entity.Name,
                totalPrice = 100 + 200 - 200 * 0.2f,
                currentProducts = afterProducts
            };
            IActionResult actual = cartController.AddProduct(p3.Id, currentProductsIds);
            IActionResult expected = new OkObjectResult(ret);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
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
            cartMock.Setup(m => m.GetCurrentProducts(currentProductsIds)).Returns(currentProducts);
            cartMock.Setup(m => m.DoPurchase());
            cartMock.Setup(m => m.GetTotalPrice()).Returns(100 + 200 - 200 * 0.2f);
            cartMock.SetupGet(m => m.PromotionApplied).Returns(promo20);
            cartMock.SetupGet(m => m.ProductsChecked).Returns(currentProducts);

            var ret = new
            {
                result = "Purchase done",
                promotionApplied = entity.Name,
                totalPrice = 100 + 200 - 200 * 0.2f,
                currentProducts = currentProductsIds
            };
            IActionResult actual = cartController.DoPurchase(currentProductsIds);
            IActionResult expected = new OkObjectResult(ret);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
        }
    }
}
