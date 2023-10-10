
using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IService<User> UserService;
        private readonly ISessionLogic SessionLogic;

        public UserLogic(IService<User> userService, ISessionLogic sessionLogic)
        {
            UserService = userService;
            SessionLogic = sessionLogic;
        }

        public void Add(User newUser)
        {
            User current = SessionLogic.GetCurrentUser();
            UserService.Add(newUser);
        }

        public void Delete(string email)
        {
            User user = new User()
            {
                Email = email
            };
            UserService.Delete(user);
        }

        public User Get(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User updatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
