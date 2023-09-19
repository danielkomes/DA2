using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Promotion3x2 : IPromotion
    {
        public float GetTotal(IEnumerable<Product> products)
        {
            float total = 0;
            foreach (Product product in products)
            {
                total += product.Price;
            }
            return total;
        }
    }
}