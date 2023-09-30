using Domain;

namespace WebApi.Models.In
{
    public class ProductModelIn
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Colors { get; set; }

        public Product ToEntity()
        {
            return new Product
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Description = Description,
                Brand = Brand,
                Category = Category,
                Colors = Colors
            };
        }
    }
}