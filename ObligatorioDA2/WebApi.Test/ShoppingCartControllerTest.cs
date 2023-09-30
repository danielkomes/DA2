using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Test
{
    [TestClass]
    public class ShoppingCartControllerTest
    {
        [TestMethod]
        public void GetAll3ProductsCheckedOk()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            IEnumerable<ProductModelOut> productModels = new List<ProductModelOut>
            {
                new ProductModelOut(p1),
                new ProductModelOut(p2),
                new ProductModelOut(p3),
            };
            var cartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            ShoppingCartController cartController = new ShoppingCartController(cartMock.Object);
            cartMock.Setup(m => m.ProductsChecked).Returns(products);


            IActionResult actual = cartController.GetProducts();
            IActionResult expected = new OkObjectResult(productModels);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            IEnumerable<ProductModelOut> actualModels = actualOk.Value as IEnumerable<ProductModelOut>;
            OkObjectResult expectedOk = expected as OkObjectResult;
            IEnumerable<ProductModelOut> expectedModels = expectedOk.Value as IEnumerable<ProductModelOut>;
            for (int i = 0; i < productModels.Count(); i++)
            {
                Assert.AreEqual(expectedModels.ElementAt(i).Name, actualModels.ElementAt(i).Name);
                Assert.AreEqual(expectedModels.ElementAt(i).Description, actualModels.ElementAt(i).Description);
            }
            cartMock.VerifyAll();
        }

        [TestMethod]
        public void RemoveProductOk()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            ProductModelIn productModel = new ProductModelIn()
            {
                Name = p1.Name,
                Description = p1.Description,
                Price = p1.Price,
                Brand = p1.Brand,
                Category = p1.Category,
                Colors = p1.Colors
            };
            var cartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            ShoppingCartController cartController = new ShoppingCartController(cartMock.Object);
            cartMock.Setup(m => m.RemoveFromCart(It.IsAny<Product>()));


            IActionResult actual = cartController.RemoveSelectedProduct(new List<ProductModelIn> { productModel });
            IActionResult expected = new OkObjectResult("Product(s) removed");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            cartMock.VerifyAll();
        }

        [TestMethod]
        public void RemoveAllProductsOk()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            ProductModelIn productModel = new ProductModelIn()
            {
                Name = p1.Name,
                Description = p1.Description,
                Price = p1.Price,
                Brand = p1.Brand,
                Category = p1.Category,
                Colors = p1.Colors
            };
            var cartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            ShoppingCartController cartController = new ShoppingCartController(cartMock.Object);
            cartMock.SetupSet(m => m.ProductsChecked = It.IsAny<IEnumerable<Product>>());


            IActionResult actual = cartController.RemoveAllProducts();
            IActionResult expected = new OkObjectResult("All products removed");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            cartMock.VerifyAll();
        }

        [TestMethod]
        public void AddProductOk()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            ProductModelIn model = new ProductModelIn()
            {
                Name = p1.Name,
                Description = p1.Description,
                Price = p1.Price,
                Brand = p1.Brand,
                Category = p1.Category,
                Colors = p1.Colors
            };
            var cartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            ShoppingCartController cartController = new ShoppingCartController(cartMock.Object);
            cartMock.Setup(m => m.AddToCart(It.IsAny<Product>()));


            IActionResult actual = cartController.AddProduct(model);
            IActionResult expected = new OkObjectResult("Product added to cart");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            cartMock.VerifyAll();
        }

        [TestMethod]
        public void DoPurchaseOk()
        {
            Product p1 = new Product();
            ProductModelIn model = new ProductModelIn()
            {
                Name = p1.Name,
                Description = p1.Description,
                Price = p1.Price,
                Brand = p1.Brand,
                Category = p1.Category,
                Colors = p1.Colors
            };
            var cartMock = new Mock<IShoppingCart>(MockBehavior.Strict);
            ShoppingCartController cartController = new ShoppingCartController(cartMock.Object);
            cartMock.Setup(m => m.DoPurchase());

            IActionResult actual = cartController.DoPurchase();
            IActionResult expected = new OkObjectResult("Purchase done");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value, actualOk.Value);
            cartMock.VerifyAll();
        }
    }
}
