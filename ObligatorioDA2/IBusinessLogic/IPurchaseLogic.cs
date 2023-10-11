using Domain;

namespace IBusinessLogic
{
    public interface IPurchaseLogic
    {
        public IEnumerable<Purchase> GetAll();
    }
}
