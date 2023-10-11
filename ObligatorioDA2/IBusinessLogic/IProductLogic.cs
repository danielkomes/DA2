using Domain;

namespace IBusinessLogic
{
    public interface IProductLogic
    {
        public IEnumerable<Product> GetAll();
        public Product Get(Guid id);

    }
}
