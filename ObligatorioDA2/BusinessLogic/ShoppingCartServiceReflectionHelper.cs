using IBusinessLogic;
using IImporters;
using IImportersServices;
using PromotionInterface;

namespace BusinessLogic
{
    public class ShoppingCartServiceReflectionHelper : IShoppingCartServiceReflectionHelper
    {
        private readonly IPromotionImporter PromotionImporter;
        private readonly IImporterService ImporterService;

        public ShoppingCartServiceReflectionHelper(
            IPromotionImporter promotionImporter,
            IImporterService importerService)
        {
            PromotionImporter = promotionImporter;
            ImporterService = importerService;
        }
        public IEnumerable<PromotionAbstract> GetPromotions()
        {
            IEnumerable<PromotionAbstract> ret = new List<PromotionAbstract>();
            IEnumerable<PromotionAbstractModelIn> promotionModels = PromotionImporter.ImportPromotions();
            foreach (PromotionAbstractModelIn model in promotionModels)
            {
                PromotionAbstract importedPromotion = ImporterService.CreatePromotionAbstract(model);
                ret = ret.Append(importedPromotion);
            }
            return ret;
        }

    }
}
