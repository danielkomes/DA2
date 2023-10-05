using Domain;

namespace IBusinessLogic
{
    public interface IShoppingCartDataAccessHelper
    {
        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids);
        public Product GetProduct(Guid id);
        public IEnumerable<PromotionAbstract> GetPromotions();
        public void InsertPurchase(Purchase purchase);
    }
}
