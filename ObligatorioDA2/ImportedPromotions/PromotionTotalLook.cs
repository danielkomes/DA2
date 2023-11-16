using PromotionInterface;

namespace Promotions
{
    public class PromotionTotalLook : PromotionAbstractModelIn
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public PromotionTotalLook() : base("Promotion Total Look", "50% off on the most expensive product")
        {
        }
        public override PromotionResultModelIn GetTotal(IEnumerable<ProductModelIn> products)
        {
            if (products.Count() == 0) return new PromotionResultModelIn(0, false);
            float total = 0;
            bool applied = false;
            float maxPrice = products.ElementAt(0).Price;
            Dictionary<string, int> colorCount = new Dictionary<string, int>();
            foreach (ProductModelIn product in products)
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
            return new PromotionResultModelIn(total, applied);
        }
    }
}