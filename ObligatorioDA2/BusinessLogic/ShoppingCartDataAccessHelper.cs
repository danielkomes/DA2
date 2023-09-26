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
            return ProductService.Exists(product);
        }
        public bool VerifyProducts(IEnumerable<Product> products)
        {
            if (!products.Any()) return false;
            return products.All(p => VerifyProduct(p));
        }

        public bool VerifyPromotion(PromotionEntity promotion)
        {
            return PromotionService.Exists(promotion);
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
                    default:
                        //TODO: throw Exception?
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
