using PromotionInterface;

namespace ImportedPromotions
{
    public class Promotion99OffOn1ProductCosting300 : PromotionAbstractModelIn
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public Promotion99OffOn1ProductCosting300() : base("Promotion99OffOn1ProductCosting300", "99% off on a product costing 300 when only 1 product is in the cart")
        {
        }


        public override PromotionResultModelIn GetTotal(IEnumerable<ProductModelIn> products)
        {
            if (products.Count() == 0) return new PromotionResultModelIn(0, false);
            float total = 0;
            bool applied = false;
            foreach (ProductModelIn product in products)
            {
                total += product.Price;
            }
            if (products.Count() == 1 && products.ElementAt(0).Price == 300)
            {
                total -= total * 0.99f;
                applied = true;
            }
            return new PromotionResultModelIn(total, applied);
        }
    }
}