using Domain;
using Domain.PaymentMethods;

namespace IBusinessLogic
{
    public interface IShoppingCartServiceDatabaseHelper
    {
        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids);
        public Product GetProduct(Guid id);
        public void InsertPurchase(Purchase purchase);
        public PaymentMethodEntity GetPaymentMethod(PaymentMethodEntity paymentMethod);
    }
}
