using PromotionInterface;

namespace ImportedPromotions
{
    public class Promotion20Off : PromotionAbstractModelIn
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public Promotion20Off() : base("Promotion 20% off", "20% off on the most expensive product")
        {
        }

        public override PromotionResultModelIn GetTotal(IEnumerable<ProductModelIn> products)
        {
            if (products.Count() == 0) return new PromotionResultModelIn(0, false);
            float total = 0;
            bool applied = false;
            float maxPrice = products.ElementAt(0).Price;
            foreach (ProductModelIn product in products)
            {
                total += product.Price;
                if (product.Price > maxPrice)
                {
                    maxPrice = product.Price;
                }
            }
            if (products.Count() >= 2)
            {
                total -= maxPrice * 0.2f;
                applied = true;
            }
            return new PromotionResultModelIn(total, applied);
        }
    }
}