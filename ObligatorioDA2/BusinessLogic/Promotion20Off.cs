using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Promotion20Off : IPromotion
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