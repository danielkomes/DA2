namespace Domain
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public PromotionEntity? Promotion { get; set; }
        public DateTime Date { get; set; }

        public Purchase() { }

        public Purchase(User user, IEnumerable<Product> products, PromotionEntity? promotion = null)
        {
            Id = new Guid();
            User = user;
            Products = products;
            Promotion = promotion;
            Date = DateTime.Now;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Purchase) return false;
            Purchase other = obj as Purchase;
            return Id == other.Id;
        }
    }
}
