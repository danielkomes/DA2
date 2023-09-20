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
        public IService<Product> ProductService { get; set; }
        public IService<User> UserService { get; set; }
        public IService<PromotionEntity> PromotionService { get; set; }

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
