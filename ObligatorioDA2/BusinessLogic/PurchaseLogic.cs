using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class PurchaseLogic : IPurchaseLogic
    {
        private readonly IService<Purchase> PurchaseService;

        public PurchaseLogic(IService<Purchase> purchaseService)
        {
            PurchaseService = purchaseService;
        }

        public IEnumerable<Purchase> GetAll()
        {
            return PurchaseService.GetAll();
        }
    }
}
