using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class ShoppingCart : IShoppingCart
    {
        public User? User { get; set; }
        public IEnumerable<Product> ProductsChecked { get; set; }
        public PromotionAbstract? PromotionApplied { get; set; }
        private readonly IShoppingCartDataAccessHelper DataAccessHelper;

        public ShoppingCart(IShoppingCartDataAccessHelper dataAccessHelper)
        {
            ProductsChecked = new List<Product>();
            DataAccessHelper = dataAccessHelper;
        }

        public IEnumerable<Product> GetCurrentProducts(IEnumerable<Guid> productIds)
        {
            IEnumerable<Product> products = DataAccessHelper.GetProducts(productIds);
            ProductsChecked = products;
            return products;
        }

        public void AddToCart(Product product)
        {
            bool valid = DataAccessHelper.VerifyProduct(product);
            if (!valid) throw new InvalidDataException("Invalid product");

            IEnumerable<Guid> listToCheck = new List<Guid>();
            foreach (Product currentProduct in ProductsChecked)
            {
                listToCheck = listToCheck.Append(currentProduct.Id);
            }
            listToCheck = listToCheck.Append(product.Id);
            GetCurrentProducts(listToCheck);
        }

        public void DoPurchase()
        {
            DataAccessHelper.VerifyUser(User);
            DataAccessHelper.VerifyProducts(ProductsChecked);
            GetTotalPrice();
            Purchase purchase = new Purchase(User, ProductsChecked, PromotionApplied?.PromotionEntity);
            DataAccessHelper.InsertPurchase(purchase);
        }

        public float GetTotalPrice()
        {
            float total = 0;

            foreach (Product product in ProductsChecked)
            {
                total += product.Price;
            }
            PromotionApplied = null;

            IEnumerable<PromotionAbstract> promotions = DataAccessHelper.GetPromotions();
            foreach (PromotionAbstract promotion in promotions)
            {
                PromotionResult result = promotion.GetTotal(ProductsChecked);
                if (result.Result < total) total = result.Result;
                if (result.IsApplied) PromotionApplied = promotion;
            }
            return total;
        }

        public void RemoveFromCart(Product product)
        {
            ProductsChecked = ProductsChecked.Where(p => p.Id != product.Id);
        }
    }
}
