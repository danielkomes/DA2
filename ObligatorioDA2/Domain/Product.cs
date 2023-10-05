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

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Product) return false;
            Product other = obj as Product;
            return Id == other.Id;
        }
    }
}