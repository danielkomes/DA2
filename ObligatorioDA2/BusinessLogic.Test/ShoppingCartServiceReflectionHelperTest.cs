using Domain;
using IBusinessLogic;
using IImportersServices;
using ImportedPromotions;
using Importers;
using Moq;
using PromotionInterface;
using Promotions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ShoppingCartServiceReflectionHelperTest
    {
        private Mock<IPromotionImporter> PromotionImporterMock;
        private Mock<IImporterService> ImporterServiceMock;

        private IShoppingCartServiceReflectionHelper Helper { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            PromotionImporterMock = new Mock<IPromotionImporter>(MockBehavior.Strict);
            ImporterServiceMock = new Mock<IImporterService>(MockBehavior.Strict);

            Helper = new ShoppingCartServiceReflectionHelper(PromotionImporterMock.Object, ImporterServiceMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PromotionImporterMock.VerifyAll();
            ImporterServiceMock.VerifyAll();
        }

        [TestMethod]
        public void GetPromotions1Promotion()
        {
            PromotionAbstractModelIn model = new Promotion99OffOn1ProductCosting300();
            PromotionEntity entity = new PromotionEntity()
            {
                Type = EPromotionType.PromotionImported
            };
            PromotionAbstract promotion = new PromotionImported(model, entity, null, null);
            IEnumerable<PromotionAbstract> promotions = new List<PromotionAbstract>() { promotion };
            IEnumerable<PromotionAbstractModelIn> promotionModels = new List<PromotionAbstractModelIn>() { model };

            PromotionImporterMock.Setup(m => m.ImportPromotions()).Returns(promotionModels);
            ImporterServiceMock.Setup(m => m.CreatePromotionAbstract(model)).Returns(promotion);

            IEnumerable<PromotionAbstract> actual = Helper.GetPromotions();
            IEnumerable<PromotionAbstract> expected = promotions;

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).PromotionEntity, actual.ElementAt(i).PromotionEntity);
            }
        }
    }
}
