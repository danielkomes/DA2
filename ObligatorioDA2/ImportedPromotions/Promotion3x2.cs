using PromotionInterface;

namespace Promotions
{
    public class Promotion3x2 : PromotionAbstractModelIn
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public Promotion3x2() : base("Promotion 3x2", "100% off on the least expensive product")
        {
        }
        public override PromotionResultModelIn GetTotal(IEnumerable<ProductModelIn> products)
        {
            if (products.Count() == 0) return new PromotionResultModelIn(0, false);
            float total = 0;
            bool applied = false;
            float minPrice = products.ElementAt(0).Price;
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            foreach (ProductModelIn product in products)
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
            return new PromotionResultModelIn(total, applied);
        }
    }
}