using Domain;

namespace IBusinessLogic
{
    public abstract class PromotionAbstract
    {
        public readonly EPromotionType Type;
        protected readonly PromotionEntity PromotionEntity;

        public PromotionAbstract(PromotionEntity promotionEntity, EPromotionType type)
        {
            PromotionEntity = promotionEntity;
            Type = type;
        }

        public abstract float GetTotal(IEnumerable<Product> products);
    }
}