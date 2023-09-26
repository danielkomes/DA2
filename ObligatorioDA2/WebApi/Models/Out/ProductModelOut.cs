using Domain;

namespace WebApi.Models.Out
{
    public class ProductModelOut
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Colors { get; set; }

        public ProductModelOut(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Brand = product.Brand;
            Category = product.Category;
            Colors = product.Colors;
        }
    }
}