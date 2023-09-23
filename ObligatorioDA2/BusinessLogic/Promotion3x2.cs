﻿using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Promotion3x2 : PromotionAbstract
    {
        private const EPromotionType TYPE = EPromotionType.Promotion3x2;
        public Promotion3x2(PromotionEntity promotionEntity) : base(promotionEntity, TYPE)
        {
        }
        public override float GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return 0;
            float total = 0;
            float minPrice = products.ElementAt(0).Price;
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            foreach (Product product in products)
            {
                total += product.Price;
                if (categoryCount.ContainsKey(product.Category))
                {
                    categoryCount[product.Category]++;
                }
                else
                {
                    categoryCount.Add(product.Category, 1);
                }
                if (product.Price < minPrice)
                {
                    minPrice = product.Price;
                }
            }
            if (categoryCount.Values.Max() >= 3)
            {
                total -= minPrice;
            }
            return total;
        }
    }
}