using Domain;

namespace IBusinessLogic
{
    public interface IShoppingCartDataAccessHelper
    {
        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids);
        public bool VerifyProduct(Product product);
        public bool VerifyProducts(IEnumerable<Product> products);
        public bool VerifyUser(User user);
        public IEnumerable<PromotionAbstract> GetPromotions();
        public void InsertPurchase(Purchase purchase);
    }
}
