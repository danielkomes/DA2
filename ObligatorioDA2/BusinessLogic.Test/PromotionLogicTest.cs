using Domain;
using IBusinessLogic;
using Moq;
using PromotionInterface;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PromotionLogicTest
    {
        private Mock<IProductLogic> ProductLogicMock { get; set; }
        private PromotionLogic PromotionLogic { get; set; }
        [TestInitialize]
        public void TestInitialize()
        {
            ProductLogicMock = new Mock<IProductLogic>(MockBehavior.Strict);
            PromotionLogic = new PromotionLogic(ProductLogicMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ProductLogicMock.VerifyAll();
        }

        [TestMethod]
        public void CreatePromotionEntityOk()
        {
            Mock<PromotionAbstractModelIn> model = new Mock<PromotionAbstractModelIn>();
            PromotionEntity entity = new PromotionEntity()
            {
                Type = EPromotionType.PromotionImported
            };

            PromotionEntity actual = PromotionLogic.CreatePromotionEntity(model.Object);
            PromotionEntity expected = entity;

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Type, actual.Type);
        }

        [TestMethod]
        public void CreatePromotionResultOk()
        {
            PromotionResultModelIn model = new PromotionResultModelIn(100, false);
            PromotionResult result = new PromotionResult(100, false);

            PromotionResult actual = PromotionLogic.CreatePromotionResult(model);
            PromotionResult expected = result;

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.IsApplied, actual.IsApplied);
        }
    }

}
