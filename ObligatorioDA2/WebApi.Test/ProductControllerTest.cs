using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Test
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void GetAll3ProductsOk()
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
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            ProductController productController = new ProductController(productMock.Object);
            productMock.Setup(m => m.GetAll()).Returns(products);


            IActionResult actual = productController.GetAll();
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
            productMock.VerifyAll();
        }

    }
}