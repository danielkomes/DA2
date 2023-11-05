namespace PromotionInterface
{
    public abstract class PromotionAbstractModelIn
    {
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public PromotionAbstractModelIn(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract PromotionResultModelIn GetTotal(IEnumerable<ProductModelIn> products);
    }
}