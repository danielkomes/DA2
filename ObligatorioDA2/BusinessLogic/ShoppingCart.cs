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
        private readonly IShoppingCartService Helper;

        public ShoppingCart(IShoppingCartService dataAccessHelper)
        {
            ProductsChecked = new List<Product>();
            Helper = dataAccessHelper;
        }

        public IEnumerable<Product> GetCurrentProducts(IEnumerable<Guid> productIds)
        {
            IEnumerable<Product> products = Helper.GetProducts(productIds);
            ProductsChecked = products;
            return products;
        }

        //public void AddToCart(Guid productId)
        //{
        //    Product productToAdd = Helper.GetProduct(productId);
        //    ProductsChecked = ProductsChecked.Append(productToAdd);
        //}

        public void DoPurchase()
        {
            if (ProductsChecked.Count() == 0) throw new InvalidDataException("Shopping cart is empty");
            float total = GetTotalPrice();
            PaymentMethod paymentMethod = Helper.GetPaymentMethod(User, PaymentMethod);
            Purchase purchase = new Purchase(User, ProductsChecked, paymentMethod.Entity, total, PromotionApplied?.PromotionEntity);
            Helper.InsertPurchase(purchase);
        }

        public float GetTotalPrice()
        {
            float total = 0;

            foreach (Product product in ProductsChecked)
            {
                total += product.Price;
            }
            PromotionApplied = null;

            IEnumerable<PromotionAbstract> promotions = Helper.GetPromotions();
            foreach (PromotionAbstract promotion in promotions)
            {
                PromotionResult result = promotion.GetTotal(ProductsChecked);
                if (result.Result < total) total = result.Result;
                if (result.IsApplied) PromotionApplied = promotion;
            }
            total = ApplyPaymentMethodDiscount(total);
            return total;
        }

        public float ApplyPaymentMethodDiscount(float total)
        {
            float ret = total;
            if (PaymentMethod == EPaymentMethodType.Paganza)
            {
                ret = total - total * 0.1f;
            }
            return ret;
        }

        //public void RemoveFromCart(Guid productId)
        //{
        //    Product toRemove = ProductsChecked.Where(p => p.Id == productId).FirstOrDefault();
        //    if (toRemove is null) throw new InvalidDataException("Product not found in cart");
        //    ProductsChecked = ProductsChecked.Where(p => p.Id != productId);
        //}
    }
}
