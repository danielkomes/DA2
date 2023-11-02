using Domain;
using IBusinessLogic;

namespace Promotions
{
    public class Promotion3x2 : PromotionAbstract
    {
        private const EPromotionType TYPE = EPromotionType.Promotion3x2;
        public Promotion3x2(PromotionEntity promotionEntity) : base(promotionEntity, TYPE)
        {
        }
        public override PromotionResult GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return new PromotionResult(0, false);
            float total = 0;
            bool applied = false;
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
                applied = true;
            }
            return new PromotionResult(total, applied);
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Promotion20Off) return false;
            Promotion3x2 other = obj as Promotion3x2;
            return PromotionEntity.Equals(other.PromotionEntity);
        }
    }
}