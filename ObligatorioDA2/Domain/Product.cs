namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Colors { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
        }

    }
}