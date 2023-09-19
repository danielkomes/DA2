using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Promotion20Off : IPromotion
    {
        public float GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return 0;
            float total = 0;
            float maxPrice = products.ElementAt(0).Price;
            foreach (Product product in products)
            {
                total += product.Price;
                if (product.Price > maxPrice)
                {
                    maxPrice = product.Price;
                }
            }
            if (products.Count() >= 2)
            {
                total -= maxPrice * 0.2f;
            }
            return total;
        }
    }
}