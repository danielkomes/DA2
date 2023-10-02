using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class ShoppingCartDataAccessHelper : IShoppingCartDataAccessHelper
    {
        private readonly IService<User> UserService;
        private readonly IService<Product> ProductService;
        private readonly IService<PromotionEntity> PromotionService;
        private readonly IService<Purchase> PurchaseService;

        public ShoppingCartDataAccessHelper(IService<User> userService,
            IService<Product> productService,
            IService<PromotionEntity> promotionService,
            IService<Purchase> purchaseService)
        {
            UserService = userService;
            ProductService = productService;
            PromotionService = promotionService;
            PurchaseService = purchaseService;
        }
        public bool VerifyProduct(Product product)
        {
            return ProductService.Exists(product);
        }
        public bool VerifyProducts(IEnumerable<Product> products)
        {
            if (!products.Any()) throw new InvalidDataException("No products in cart");
            return products.All(p => VerifyProduct(p));
        }
        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids)
        {
            IEnumerable<Product> products = new List<Product>();
            foreach (Guid id in ids)
            {
                Product p = new Product()
                {
                    Id = id
                };
                products = products.Append(ProductService.Get(p));
            }
            return products;
        }

        public bool VerifyUser(User user)
        {
            return UserService.Exists(user);
        }

        public IEnumerable<PromotionAbstract> GetPromotions()
        {
            IEnumerable<PromotionEntity> entities = PromotionService.GetAll();
            IEnumerable<PromotionAbstract> ret = new List<PromotionAbstract>();
            foreach (PromotionEntity entity in entities)
            {
                switch (entity.Type)
                {
                    case EPromotionType.Promotion20Off:
                        ret = ret.Append(new Promotion20Off(entity));
                        break;
                    case EPromotionType.Promotion3x2:
                        ret = ret.Append(new Promotion3x2(entity));
                        break;
                    case EPromotionType.PromotionTotalLook:
                        ret = ret.Append(new PromotionTotalLook(entity));
                        break;
                }
            }
            return ret;
        }

        public void InsertPurchase(Purchase purchase)
        {
            PurchaseService.Add(purchase);
        }

    }
}
