using Domain;
using PromotionInterface;

namespace IBusinessLogic
{
    public interface IPromotionLogic
    {
        public PromotionEntity CreatePromotionEntity(PromotionAbstractModelIn model);
        public PromotionResult CreatePromotionResult(PromotionResultModelIn model);
    }
}
