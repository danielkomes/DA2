using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return ProductService.Exists(product.Id);
        }
        public bool VerifyProducts(IEnumerable<Product> products)
        {
            if (!products.Any()) return false;
            return products.All(p => VerifyProduct(p));
        }

        public bool VerifyPromotion(PromotionEntity promotion)
        {
            return PromotionService.Exists(promotion.Id);
        }

        public bool VerifyUser(User user)
        {
            return UserService.Exists(user.Id);
        }

        public IEnumerable<PromotionAbstract> GetPromotions()
        {
            return new List<PromotionAbstract>();
        }

        public void InsertPurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }

    }
}
