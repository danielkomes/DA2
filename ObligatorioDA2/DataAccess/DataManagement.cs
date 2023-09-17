using Domain;
using IBusinessLogic;
using IDataAccess;

namespace DataAccess
{
    public class DataManagement : IDataManagement
    {
        public void AddPurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User current, User updated)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetPurchases(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}