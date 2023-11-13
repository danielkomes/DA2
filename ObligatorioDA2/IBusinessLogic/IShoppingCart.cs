using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;

namespace IBusinessLogic
{
    public interface IShoppingCart
    {
        public User User { get; set; }
        public IEnumerable<Product> ProductsChecked { get; set; }
        public PromotionAbstract? PromotionApplied { get; set; }
        public EPaymentMethodType PaymentMethod { get; set; }


        public IEnumerable<Product> GetCurrentProducts(IEnumerable<Guid> productIds);
        public void AddToCart(Guid productId);
        public void RemoveFromCart(Guid productId);
        public float GetTotalPrice();
        public void DoPurchase();
    }
}