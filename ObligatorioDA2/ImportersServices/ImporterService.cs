using BusinessLogic;
using Domain;
using IBusinessLogic;
using IImportersServices;
using PromotionInterface;

namespace ImportersServices
{
    public class ImporterService : IImporterService
    {
        private readonly IProductLogic ProductLogic;
        private readonly IPromotionLogic PromotionLogic;
        public ImporterService(IProductLogic productLogic, IPromotionLogic promotionLogic)
        {
            ProductLogic = productLogic;
            PromotionLogic = promotionLogic;
        }
        public PromotionAbstract CreatePromotionAbstract(PromotionAbstractModelIn model)
        {
            PromotionEntity promotionEntity = PromotionLogic.CreatePromotionEntity(model);
            return new PromotionImported(model, promotionEntity, ProductLogic, PromotionLogic);
        }
    }
}