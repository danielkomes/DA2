using Domain;
using IBusinessLogic;
using PromotionInterface;

namespace BusinessLogic
{
    public class PromotionLogic : IPromotionLogic
    {
        private readonly IProductLogic ProductLogic;

        public PromotionLogic(IProductLogic productLogic) { }

        public PromotionEntity CreatePromotionEntity(PromotionAbstractModelIn model)
        {
            return new PromotionEntity()
            {
                Name = model.Name,
                Description = model.Description,
                Type = EPromotionType.PromotionImported,
            };
        }

        public PromotionResult CreatePromotionResult(PromotionResultModelIn model)
        {
            return new PromotionResult(model.Result, model.IsApplied);
        }
    }
}
