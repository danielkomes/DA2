using Domain;

namespace IBusinessLogic
{
    public interface IShoppingCart
    {
        public User User { get; set; }
        public IEnumerable<Product> ProductsChecked { get; set; }
        public PromotionAbstract? PromotionApplied { get; set; }


        public IEnumerable<Product> GetCurrentProducts(IEnumerable<Guid> productIds);
        public void AddToCart(Product product);
        public void RemoveFromCart(Product product);
        public float GetTotalPrice();
        public void DoPurchase();
    } 
}