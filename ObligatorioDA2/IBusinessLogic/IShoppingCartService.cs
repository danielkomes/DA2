using Domain;
using Domain.PaymentMethods.BaseClasses;
using Domain.PaymentMethods;

namespace IBusinessLogic
{
    public interface IShoppingCartService
    {
        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids);
        public Product GetProduct(Guid id);
        public IEnumerable<PromotionAbstract> GetPromotions();
        public void InsertPurchase(Purchase purchase);
        public PaymentMethod GetPaymentMethod(User user, EPaymentMethodType paymentMethod);
    }
}
