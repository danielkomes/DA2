using BusinessLogic.Exceptions;
using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class SignupLogic : ISignupLogic
    {
        private readonly IService<User> UserService;

        public SignupLogic(IService<User> userService)
        {
            UserService = userService;
        }

        public void Signup(User user)
        {
            if (UserService.Exists(user))
            {
                throw new EntityAlreadyExistsException("Email already exists");
            }
            UserService.Add(user);
        }
    }
}
