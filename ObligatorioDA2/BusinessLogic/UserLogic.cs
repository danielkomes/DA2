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
            User current = SessionLogic.GetCurrentUser();
            if (!current.Roles.Contains(EUserRole.Admin))
            {
                if (!current.Email.Equals(targetEmail))
                {
                    throw new ProfileMismatchException("Profile mismatch");
                }
            }
            bool exists = UserService.Exists(updatedUser);
            if (exists) //email already exists
            {
                User userInDb = UserService.Get(updatedUser);

                //check if the existing email belongs to the logged user (user is not updating the email)
                bool sameId = userInDb.Id == current.Id;
                if (!sameId) //existing email does not belong to the logged user
                {
                    throw new EntityAlreadyExistsException("Email already exists");
                }

            }
            UserService.Update(updatedUser);
        }
    }
}
