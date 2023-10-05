using Domain;

namespace IBusinessLogic
{
    public interface IUserLogic
    {
        public IEnumerable<User> GetAll();
        public User Get(string email);
        public void Add(User newUser);
        public void Update(User updatedUser);
        public void Delete(string email);
    }
}
