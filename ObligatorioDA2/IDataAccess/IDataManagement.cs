using Domain;
using IBusinessLogic;

namespace IDataAccess
{
    public interface IDataManagement
    {
        public void AddPurchase(Purchase purchase);
        public IEnumerable<Purchase> GetPurchases(User user);
        public IEnumerable<User> GetUsers();
        public void AddUser(User user);
        public void EditUser(User current, User updated);
        public void DeleteUser(User user);
    }
}