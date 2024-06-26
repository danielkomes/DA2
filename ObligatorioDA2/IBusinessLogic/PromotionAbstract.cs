﻿using Domain;

namespace IBusinessLogic
{
    public abstract class PromotionAbstract
    {
        public readonly EPromotionType Type;
        public readonly PromotionEntity PromotionEntity;

        public PromotionAbstract(PromotionEntity promotionEntity, EPromotionType type)
        {
            PromotionEntity = promotionEntity;
            Type = type;
        }

        public abstract PromotionResult GetTotal(IEnumerable<Product> products);
    }
}