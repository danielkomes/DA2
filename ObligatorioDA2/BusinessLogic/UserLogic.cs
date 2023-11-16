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

        public void Delete(string targetEmail)
        {
            User current = SessionLogic.GetCurrentUser();
            User targetUser = new User()
            {
                Email = targetEmail
            };
            targetUser = UserService.Get(targetUser);
            if (!current.Email.Equals(targetEmail))
            {
                if (!current.Roles.Contains(EUserRole.Admin))
                {
                    throw new ProfileMismatchException("Profile mismatch");
                }


                if (targetUser.Roles.Contains(EUserRole.Admin))
                {
                    throw new ProfileMismatchException("Profile mismatch");
                }
            }
            UserService.Delete(targetUser);
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
            User targetUser = new User()
            {
                Email = targetEmail
            };
            targetUser = UserService.Get(targetUser);
            updatedUser.Id = targetUser.Id;
            if (!current.Email.Equals(targetEmail))
            {
                if (!current.Roles.Contains(EUserRole.Admin))
                {
                    throw new ProfileMismatchException("Profile mismatch");
                }


                if (targetUser.Roles.Contains(EUserRole.Admin))
                {
                    throw new ProfileMismatchException("Profile mismatch");
                }
            }
            if (!updatedUser.Email.Equals(targetEmail))
            {
                bool exists = UserService.Exists(updatedUser);
                if (exists)
                {
                    throw new EntityAlreadyExistsException("Email already exists");

                }
            }
            UserService.Update(updatedUser);
        }
    }
}
