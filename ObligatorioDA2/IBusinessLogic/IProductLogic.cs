using Domain;
using PromotionInterface;

namespace IBusinessLogic
{
    public interface IProductLogic
    {
        public IEnumerable<Product> GetAll();
        public Product Get(Guid id);
        public IEnumerable<Product> FindByCondition(string? name, string? brand, string? category);
        public ProductModelIn CreateProductModelIn(Product product);

    }
}
