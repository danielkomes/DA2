using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Test
{
    [TestClass]
    public class ProductControllerTest
    {
        private Mock<IProductLogic> ProductMock { get; set; }
        private ProductController ProductController { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ProductMock = new Mock<IProductLogic>(MockBehavior.Strict);
            ProductController = new ProductController(ProductMock.Object);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            ProductMock.VerifyAll();
        }

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
            ProductMock.Setup(m => m.FindByCondition(null, null, null)).Returns(products);


            IActionResult actual = ProductController.GetAll();
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
        }

        [TestMethod]
        public void Get1ProductOk()
        {
            Product p1 = new Product();
            ProductMock.Setup(m => m.Get(p1.Id)).Returns(p1);


            IActionResult actual = ProductController.Get(p1.Id);
            IActionResult expected = new OkObjectResult(new ProductModelOut(p1));

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            OkObjectResult expectedOk = expected as OkObjectResult;
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
        }

        [TestMethod]
        public void GetProductsByNameOk()
        {
            Product p1 = new Product()
            {
                Name = "p1"
            };
            Product p2 = new Product()
            {
                Name = "p1"
            };
            Product p3 = new Product()
            {
                Name = "p2"
            };
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            IEnumerable<ProductModelOut> productModels = new List<ProductModelOut>
            {
                new ProductModelOut(p1),
                new ProductModelOut(p2),
            };
            ProductMock.Setup(m => m.FindByCondition("p1", null, null)).Returns(products);
            ProductFilterModelIn filter = new ProductFilterModelIn(name: "p1");

            IActionResult actual = ProductController.GetAll(filter);
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
        }

        [TestMethod]
        public void GetProductsByPartialNameOk()
        {
            Product p1 = new Product()
            {
                Name = "p1"
            };
            Product p2 = new Product()
            {
                Name = "p1"
            };
            Product p3 = new Product()
            {
                Name = "p2"
            };
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            IEnumerable<ProductModelOut> productModels = new List<ProductModelOut>
            {
                new ProductModelOut(p1),
                new ProductModelOut(p2),
            };
            ProductMock.Setup(m => m.FindByCondition("1", null, null)).Returns(products);
            ProductFilterModelIn filter = new ProductFilterModelIn(name: "1");

            IActionResult actual = ProductController.GetAll(filter);
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
        }

        [TestMethod]
        public void GetProductsByNameAndCategoryOk()
        {
            Product p1 = new Product()
            {
                Name = "p1",
                Category = "cat1"
            };
            Product p2 = new Product()
            {
                Name = "p1",
                Category = "cat2"
            };
            Product p3 = new Product()
            {
                Name = "p2"
            };
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            IEnumerable<ProductModelOut> productModels = new List<ProductModelOut>
            {
                new ProductModelOut(p1),
            };
            ProductMock.Setup(m => m.FindByCondition("p1", null, "cat1")).Returns(products);
            ProductFilterModelIn filter = new ProductFilterModelIn(name: "p1", category: "cat1");

            IActionResult actual = ProductController.GetAll(filter);
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
        }

        [TestMethod]
        public void GetProductsByNameNoMatch()
        {
            Product p1 = new Product()
            {
                Name = "p1"
            };
            Product p2 = new Product()
            {
                Name = "p1"
            };
            Product p3 = new Product()
            {
                Name = "p2"
            };
            IEnumerable<Product> products = new List<Product> { p1, p2, p3 };
            IEnumerable<ProductModelOut> productModels = new List<ProductModelOut>
            {
                new ProductModelOut(p1),
                new ProductModelOut(p2),
            };
            ProductMock.Setup(m => m.FindByCondition("non-matching product", null, null)).Returns(new List<Product>());
            ProductFilterModelIn filter = new ProductFilterModelIn(name: "non-matching product");

            IActionResult actual = ProductController.GetAll(filter);
            IActionResult expected = new OkObjectResult(new List<ProductModelOut>());

            Assert.AreEqual(expected.GetType(), actual.GetType());
            OkObjectResult actualOk = actual as OkObjectResult;
            IEnumerable<ProductModelOut> actualModels = actualOk.Value as IEnumerable<ProductModelOut>;
            OkObjectResult expectedOk = expected as OkObjectResult;
            IEnumerable<ProductModelOut> expectedModels = expectedOk.Value as IEnumerable<ProductModelOut>;
            Assert.AreEqual(expectedModels.Count(), actualModels.Count());
            Assert.AreEqual(expectedOk.Value.ToString(), actualOk.Value.ToString());
        }
    }
}