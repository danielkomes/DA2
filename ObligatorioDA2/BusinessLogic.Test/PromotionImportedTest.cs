using Domain;
using IBusinessLogic;
using Moq;
using PromotionInterface;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PromotionImportedTest
    {
        private Mock<PromotionAbstractModelIn> PromotionModelMock { get; set; }
        private Mock<IProductLogic> ProductLogicMock { get; set; }
        private Mock<IPromotionLogic> PromotionLogicMock { get; set; }
        private PromotionEntity PromotionEntity { get; set; }
        private PromotionAbstract PromotionImported { get; set; }
        [TestInitialize]
        public void TestInitialize()
        {
            PromotionModelMock = new Mock<PromotionAbstractModelIn>(MockBehavior.Strict);
            ProductLogicMock = new Mock<IProductLogic>(MockBehavior.Strict);
            PromotionLogicMock = new Mock<IPromotionLogic>(MockBehavior.Strict);

            PromotionEntity = new PromotionEntity()
            {
                Type = EPromotionType.PromotionImported
            };
            PromotionImported = new PromotionImported(PromotionModelMock.Object, PromotionEntity, ProductLogicMock.Object, PromotionLogicMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PromotionModelMock.VerifyAll();
            ProductLogicMock.VerifyAll();
            PromotionLogicMock.VerifyAll();
        }

        [TestMethod]
        public void GetTotal2Products()
        {
            Product p1 = new Product()
            {
                Price = 100
            };
            Product p2 = new Product()
            {
                Price = 200
            };

            ProductModelIn model1 = new ProductModelIn()
            {
                Price = p1.Price
            };
            ProductModelIn model2 = new ProductModelIn()
            {
                Price = p2.Price
            };
            IEnumerable<Product> products = new List<Product>() { p1, p2 };
            IEnumerable<ProductModelIn> models = new List<ProductModelIn>() { model1, model2 };
            PromotionResultModelIn resultModel = new PromotionResultModelIn(300, false);
            PromotionResult result = new PromotionResult(300, false);

            ProductLogicMock.Setup(m => m.CreateProductModelIn(p1)).Returns(model1);
            ProductLogicMock.Setup(m => m.CreateProductModelIn(p2)).Returns(model2);
            PromotionModelMock.Setup(m => m.GetTotal(models)).Returns(resultModel);
            PromotionLogicMock.Setup(m => m.CreatePromotionResult(resultModel)).Returns(result);

            PromotionResult actual = PromotionImported.GetTotal(products);
            PromotionResult expected = result;

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }
    }

}
