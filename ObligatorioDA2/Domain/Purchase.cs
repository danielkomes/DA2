namespace Domain
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public PromotionEntity? Promotion { get; set; }
        public DateTime Date { get; set; }

        public Purchase(User user, IEnumerable<Product> products, PromotionEntity promotion = null)
        {
            User = user;
            Products = products;
            Id = new Guid();
            Date = DateTime.Now;
        }
    }
}
