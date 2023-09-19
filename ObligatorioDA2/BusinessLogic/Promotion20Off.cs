using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Promotion20Off : IPromotion
    {

        public float GetTotal(IEnumerable<Product> products)
        {
            return 0;
        }
    }
}