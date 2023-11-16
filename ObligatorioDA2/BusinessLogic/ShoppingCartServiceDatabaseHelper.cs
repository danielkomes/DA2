using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class ShoppingCartServiceDatabaseHelper : IShoppingCartServiceDatabaseHelper
    {
        private readonly IService<Product> ProductService;
        private readonly IService<PromotionEntity> PromotionService;
        private readonly IService<Purchase> PurchaseService;
        private readonly IService<PaymentMethodEntity> PaymentMethodService;

        public ShoppingCartServiceDatabaseHelper(
            IService<Product> productService,
            IService<PromotionEntity> promotionService,
            IService<Purchase> purchaseService,
            IService<PaymentMethodEntity> paymentMethodService
            )
        {
            ProductService = productService;
            PromotionService = promotionService;
            PurchaseService = purchaseService;
            PaymentMethodService = paymentMethodService;
        }

        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids)
        {
            IEnumerable<Product> products = new List<Product>();
            foreach (Guid id in ids)
            {
                products = products.Append(GetProduct(id));
            }
            return products;
        }

        public Product GetProduct(Guid id)
        {
            Product p = new Product()
            {
                Id = id
            };
            return ProductService.Get(p);
        }

        public PaymentMethodEntity GetPaymentMethod(PaymentMethodEntity paymentMethod)
        {
            return PaymentMethodService.Get(paymentMethod);
        }

        public void InsertPurchase(Purchase purchase)
        {
            PurchaseService.Add(purchase);
            bool paymentMethodExists = PaymentMethodService.Exists(purchase.PaymentMethod);
            if (!paymentMethodExists)
            {
                PaymentMethodService.Add(purchase.PaymentMethod);
            }
        }
    }
}
