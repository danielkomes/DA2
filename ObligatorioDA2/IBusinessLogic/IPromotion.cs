using Domain;

namespace IBusinessLogic
{
    public interface IPromotion
    {
        public float GetTotal(IEnumerable<Product> products);
    } 
}