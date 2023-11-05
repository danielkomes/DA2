using DataAccess.Exceptions;
using Domain;
using IBusinessLogic;
using IDataAccess;
using Moq;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ShoppingCartTest
    {

        [TestMethod]
        public void AddToCartCorrect()
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
            IEnumerable<Guid> currentProductsIds = new List<Guid> { p1.Id, p2.Id };
            IEnumerable<Guid> afterProducts = new List<Guid> { p1.Id, p2.Id, p3.Id };
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCart shoppingCart = new ShoppingCart(helperMock.Object);
            helperMock.Setup(m => m.GetProduct(p1.Id)).Returns(p1);
            helperMock.Setup(m => m.GetProduct(p2.Id)).Returns(p2);
            helperMock.Setup(m => m.GetProduct(p3.Id)).Returns(p3);

            shoppingCart.AddToCart(p1.Id);
            shoppingCart.AddToCart(p2.Id);
            shoppingCart.AddToCart(p3.Id);

            IEnumerable<Product> actual = shoppingCart.ProductsChecked;
            IEnumerable<Product> expected = currentProducts;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Price, actual.ElementAt(i).Price);
                Assert.AreEqual(expected.ElementAt(i).Category, actual.ElementAt(i).Category);
                Assert.AreEqual(expected.ElementAt(i).Colors.First(), actual.ElementAt(i).Colors.First());
            }
            helperMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void AddToCartInvalid()
        {
            Product p = new Product();
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(m => m.GetProduct(p.Id)).Throws(new ResourceNotFoundException("Product not found"));
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p.Id);

            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetProducts3Products()
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
            IEnumerable<Product> currentProducts = new List<Product> { p1, p2, p3 };
            IEnumerable<Guid> currentProductsIds = new List<Guid> { p1.Id, p2.Id, p3.Id };
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCart shoppingCart = new ShoppingCart(helperMock.Object);
            helperMock.Setup(m => m.GetProducts(currentProductsIds)).Returns(currentProducts);


            IEnumerable<Product> actual = shoppingCart.GetCurrentProducts(currentProductsIds);
            IEnumerable<Product> expected = currentProducts;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Price, actual.ElementAt(i).Price);
                Assert.AreEqual(expected.ElementAt(i).Category, actual.ElementAt(i).Category);
                Assert.AreEqual(expected.ElementAt(i).Colors.First(), actual.ElementAt(i).Colors.First());
            }
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPriceNoProductsNoPromotions()
        {
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionAbstract>());

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            float actual = cart.GetTotalPrice();
            float expected = 0;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
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
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(new List<PromotionAbstract>());
            helperMock.Setup(sp => sp.GetProduct(p.Id)).Returns(p);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p.Id);
            float actual = cart.GetTotalPrice();
            float expected = 100;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice2ProductPromotion20Off()
        {
            Product p1 = new Product()
            {
                Price = 100
            };
            Product p2 = new Product()
            {
                Price = 200
            };
            PromotionEntity promotionEntity = new PromotionEntity();
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off
            };

            IEnumerable<Product> products = new List<Product>() { p1, p2 };
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.GetProduct(p1.Id)).Returns(p1);
            helperMock.Setup(sp => sp.GetProduct(p2.Id)).Returns(p2);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1.Id);
            cart.AddToCart(p2.Id);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 - 200 * 0.2f;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice2Product2Promotion()
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
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            Promotion3x2 promotion3x2 = new Promotion3x2(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off, promotion3x2
            };
            IEnumerable<Product> products = new List<Product>() { p1, p2 };
            IEnumerable<Guid> productIds = new List<Guid>() { p1.Id, p2.Id };
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.GetProduct(p1.Id)).Returns(p1);
            helperMock.Setup(sp => sp.GetProduct(p2.Id)).Returns(p2);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1.Id);
            cart.AddToCart(p2.Id);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 - 200 * 0.2f;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice3Product2Promotion()
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
            Product p3 = new Product()
            {
                Price = 300,
                Category = "cat1"
            };
            PromotionEntity promotionEntity = new PromotionEntity();
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            Promotion3x2 promotion3x2 = new Promotion3x2(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off, promotion3x2
            };

            IEnumerable<Product> products = new List<Product>() { p1, p2, p3 };
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.GetProduct(p1.Id)).Returns(p1);
            helperMock.Setup(sp => sp.GetProduct(p2.Id)).Returns(p2);
            helperMock.Setup(sp => sp.GetProduct(p3.Id)).Returns(p3);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1.Id);
            cart.AddToCart(p2.Id);
            cart.AddToCart(p3.Id);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 + 300 - 100;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotalPrice3Product3Promotion()
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
            Promotion20Off promotion20Off = new Promotion20Off(promotionEntity);
            Promotion3x2 promotion3x2 = new Promotion3x2(promotionEntity);
            PromotionTotalLook promotionTotalLook = new PromotionTotalLook(promotionEntity);
            List<PromotionAbstract> promotionsList = new List<PromotionAbstract> {
                promotion20Off, promotion3x2, promotionTotalLook
            };

            IEnumerable<Product> products = new List<Product>() { p1, p2, p3 };
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetPromotions())
                .Returns(promotionsList);
            helperMock.Setup(sp => sp.GetProduct(p1.Id)).Returns(p1);
            helperMock.Setup(sp => sp.GetProduct(p2.Id)).Returns(p2);
            helperMock.Setup(sp => sp.GetProduct(p3.Id)).Returns(p3);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1.Id);
            cart.AddToCart(p2.Id);
            cart.AddToCart(p3.Id);
            float actual = cart.GetTotalPrice();
            float expected = 100 + 200 + 300 - 300 * 0.5f;
            Assert.AreEqual(expected, actual);
            helperMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RemoveFromCartEmpty()
        {
            Product p1 = new Product();
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.RemoveFromCart(p1.Id);
            List<Product> actual = cart.ProductsChecked.ToList();
            Assert.IsTrue(!actual.Any());
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void RemoveFromCart1Product()
        {
            Product p1 = new Product();
            IEnumerable<Product> products = new List<Product>() { p1 };
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            helperMock.Setup(sp => sp.GetProduct(p1.Id)).Returns(p1);
            cart.AddToCart(p1.Id);
            cart.RemoveFromCart(p1.Id);
            List<Product> actual = cart.ProductsChecked.ToList();
            Assert.IsTrue(!actual.Any());
            helperMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RemoveFromCart1ProductNotAdded()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            IEnumerable<Product> products = new List<Product>() { p1 };
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(sp => sp.GetProduct(p1.Id)).Returns(p1);

            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1.Id);
            cart.RemoveFromCart(p2.Id);
            List<Product> actual = cart.ProductsChecked.ToList();
            Assert.AreEqual(1, actual.Count());
            helperMock.VerifyAll();
        }

        [TestMethod]
        public void DoPurchase1Product()
        {
            Product p1 = new Product();
            List<Product> products = new List<Product> { p1 };
            List<Guid> productIds = new List<Guid> { p1.Id };
            var productMock = new Mock<IService<Product>>(MockBehavior.Strict);
            var promotionMock = new Mock<IService<PromotionEntity>>(MockBehavior.Strict);
            var purchaseMock = new Mock<IService<Purchase>>(MockBehavior.Strict);
            var helperMock = new Mock<IShoppingCartDataAccessHelper>(MockBehavior.Strict);
            IShoppingCartDataAccessHelper helper = new ShoppingCartDataAccessHelper(
                productMock.Object, promotionMock.Object, purchaseMock.Object);
            helperMock.Setup(h => h.GetProduct(p1.Id)).Returns(p1);
            helperMock.Setup(h => h.GetPromotions()).Returns(new List<PromotionAbstract>());

            helperMock.Setup(h => h.InsertPurchase(It.IsAny<Purchase>()));
            ShoppingCart cart = new ShoppingCart(helperMock.Object);
            cart.AddToCart(p1.Id);
            cart.DoPurchase();

            helperMock.VerifyAll();
        }
    }
}
