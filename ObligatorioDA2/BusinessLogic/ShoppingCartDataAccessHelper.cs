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

        public ShoppingCartDataAccessHelper(IService<User> userService,
            IService<Product> productService,
            IService<PromotionEntity> promotionService)
        {
            UserService = userService;
            ProductService = productService;
            PromotionService = promotionService;
        }
        public bool VerifyProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPromotion(PromotionEntity promotion)
        {
            throw new NotImplementedException();
        }

        public bool VerifyUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
