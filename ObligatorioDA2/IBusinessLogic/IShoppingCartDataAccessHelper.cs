using Domain;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLogic
{
    public interface IShoppingCartDataAccessHelper
    {
        //protected IService<User> UserService { get; set; }
        //protected IService<Product> ProductService { get; set; }
        //protected IService<PromotionEntity> PromotionService { get; set; }

        public bool VerifyProduct(Product product);
        public bool VerifyUser(User user);
        public bool VerifyPromotion(PromotionEntity promotion);
        public IEnumerable<PromotionAbstract> GetPromotions();
    }
}
