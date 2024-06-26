﻿using Domain;
using IBusinessLogic;

namespace Promotions
{
    public class PromotionTotalLook : PromotionAbstract
    {
        private const EPromotionType TYPE = EPromotionType.PromotionTotalLook;
        public PromotionTotalLook(PromotionEntity promotionEntity) : base(promotionEntity, TYPE)
        {
        }
        public override PromotionResult GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return new PromotionResult(0, false);
            float total = 0;
            bool applied = false;
            float maxPrice = products.ElementAt(0).Price;
            Dictionary<string, int> colorCount = new Dictionary<string, int>();
            foreach (Product product in products)
            {
                total += product.Price;
                foreach (string color in product.Colors)
                {
                    if (colorCount.ContainsKey(color))
                    {
                        colorCount[color]++;
                    }
                    else
                    {
                        colorCount.Add(color, 1);
                    }
                }
                if (product.Price > maxPrice)
                {
                    maxPrice = product.Price;
                }
            }
            if (colorCount.Values.Max() >= 3)
            {
                total -= maxPrice * 0.5f;
                applied = true;
            }
            return new PromotionResult(total, applied);
        }
    }
}