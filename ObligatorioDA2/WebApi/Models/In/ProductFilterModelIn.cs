namespace WebApi.Models.In
{
    public class ProductFilterModelIn
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }

        public ProductFilterModelIn(string? name = null, string? brand = null, string? category = null)
        {
            Name = name;
            Brand = brand;
            Category = category;
        }

        public ProductFilterModelIn() { }
    }
}
