using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;
using IBusinessLogic;
using System.Data;

namespace BusinessLogic
{
    public class ShoppingCart : IShoppingCart
    {
        public User? User { get; set; }
        public IEnumerable<Product> ProductsChecked { get; set; }
        public PromotionAbstract? PromotionApplied { get; set; }
        public EPaymentMethodType PaymentMethod { get; set; }
        private readonly IShoppingCartService DataAccessHelper;

        public ShoppingCart(IShoppingCartService dataAccessHelper)
        {
            ProductsChecked = new List<Product>();
            DataAccessHelper = dataAccessHelper;
        }

        public IEnumerable<Product> GetCurrentProducts(IEnumerable<Guid> productIds)
        {
            IEnumerable<Product> products = DataAccessHelper.GetProducts(productIds);
            ProductsChecked = products;
            return products;
        }

        public void AddToCart(Guid productId)
        {
            Product productToAdd = DataAccessHelper.GetProduct(productId);
            ProductsChecked = ProductsChecked.Append(productToAdd);
        }

        public void DoPurchase()
        {
            if (ProductsChecked.Count() == 0) throw new InvalidDataException("Shopping cart is empty");
            float total = GetTotalPrice();
            PaymentMethodEntity paymentMethod = DataAccessHelper.GetPaymentMethod(User, PaymentMethod);
            Purchase purchase = new Purchase(User, ProductsChecked, paymentMethod, total, PromotionApplied?.PromotionEntity);
            DataAccessHelper.InsertPurchase(purchase);
        }

        public float GetTotalPrice()
        {
            float total = 0;

            foreach (Product product in ProductsChecked)
            {
                total += product.Price;
            }
            PromotionApplied = null;

            IEnumerable<PromotionAbstract> promotions = DataAccessHelper.GetPromotions();
            foreach (PromotionAbstract promotion in promotions)
            {
                PromotionResult result = promotion.GetTotal(ProductsChecked);
                if (result.Result < total) total = result.Result;
                if (result.IsApplied) PromotionApplied = promotion;
            }
            if (PaymentMethod == EPaymentMethodType.Paganza)
            {
                total = ApplyPaganzaDiscount(total);
            }
            return total;
        }

        public float ApplyPaganzaDiscount(float total)
        {
            return total - total * 0.1f;
        }

        public void RemoveFromCart(Guid productId)
        {
            Product toRemove = ProductsChecked.Where(p => p.Id == productId).FirstOrDefault();
            if (toRemove is null) throw new InvalidDataException("Product not found in cart");
            ProductsChecked = ProductsChecked.Where(p => p.Id != productId);
        }
    }
}
