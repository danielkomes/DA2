using Domain;
using Domain.PaymentMethods;

namespace IBusinessLogic
{
    public interface IShoppingCart
    {
        public User User { get; set; }
        public IEnumerable<Product> ProductsChecked { get; set; }
        public PromotionAbstract? PromotionApplied { get; set; }
        public EPaymentMethodType PaymentMethod { get; set; }


        public IEnumerable<Product> GetCurrentProducts(IEnumerable<Guid> productIds);
        public float GetTotalPrice();
        public void DoPurchase();
        public float ApplyPaymentMethodDiscount(float total);
    }
}