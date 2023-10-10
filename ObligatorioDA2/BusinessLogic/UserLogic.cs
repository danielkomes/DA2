using BusinessLogic.Exceptions;
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
            if (UserService.Exists(newUser))
            {
                throw new EntityAlreadyExistsException("Email already exists");
            }
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
            User user = new User()
            {
                Email = email
            };
            User current = SessionLogic.GetCurrentUser();
            if (!current.Roles.Contains(EUserRole.Admin))
            {
                if (!current.Email.Equals(email))
                {
                    throw new ProfileMismatchException("Profile mismatch");
                }
            }
            return UserService.Get(user);
        }

        public IEnumerable<User> GetAll()
        {
            return UserService.GetAll();
        }

        public void Update(string targetEmail, User updatedUser)
        {
            UserService.Update(updatedUser);
        }
    }
}
