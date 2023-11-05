using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartServiceDatabaseHelper DatabaseHelper;
        private readonly IShoppingCartServiceReflectionHelper ReflectionHelper;

        public ShoppingCartService(
            IShoppingCartServiceDatabaseHelper databaseHelper,
            IShoppingCartServiceReflectionHelper reflectionHelper
            )
        {
            DatabaseHelper = databaseHelper;
            ReflectionHelper = reflectionHelper;
        }

        public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids)
        {
            return DatabaseHelper.GetProducts(ids);
        }

        public Product GetProduct(Guid id)
        {
            return DatabaseHelper.GetProduct(id);
        }

        public IEnumerable<PromotionAbstract> GetPromotions()
        {
            IEnumerable<PromotionAbstract> ret = new List<PromotionAbstract>();
            foreach (PromotionAbstract promotion in DatabaseHelper.GetPromotions())
            {
                ret = ret.Append(promotion);
            }
            foreach (PromotionAbstract promotion in ReflectionHelper.GetPromotions())
            {
                ret = ret.Append(promotion);
            }
            return ret;
        }

        public void InsertPurchase(Purchase purchase)
        {
            DatabaseHelper.InsertPurchase(purchase);
        }

    }
}
