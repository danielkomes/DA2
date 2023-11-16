using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;

namespace IBusinessLogic
{
    public interface IShoppingCartService
    {
        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids);
        public IEnumerable<PromotionAbstract> GetPromotions();
        public void InsertPurchase(Purchase purchase);
        public PaymentMethod GetPaymentMethod(User user, EPaymentMethodType paymentMethod);
    }
}
