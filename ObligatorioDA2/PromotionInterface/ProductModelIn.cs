namespace PromotionInterface
{
    public class ProductModelIn
    {
        public float Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}
