using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Promotion3x2 : IPromotion
    {
        public float GetTotal(IEnumerable<Product> products)
        {
            if (products.Count() == 0) return 0;
            float total = 0;
            float minPrice = products.ElementAt(0).Price;
            foreach (Product product in products)
            {
                total += product.Price;
                if (product.Price < minPrice)
                {
                    minPrice = product.Price;
                }
            }
            if (products.Count() >= 3)
            {
                total -= minPrice;
            }
            return total;
        }
    }
}