using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLogic
{
    public interface IShoppingCartServiceDatabaseHelper
    {
        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids);
        public Product GetProduct(Guid id);
        public IEnumerable<PromotionAbstract> GetPromotions();
        public void InsertPurchase(Purchase purchase);
    }
}
