using Domain;
using IBusinessLogic;
using IDataAccess;
using System.Security.Authentication;

namespace BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private User? CurrentUser { get; set; }
        private readonly IService<Session> SessionService;
        private readonly IService<User> UserService;
        public SessionLogic(IService<Session> sessionService, IService<User> userService)
        {
            SessionService = sessionService;
            UserService = userService;
        }
        public Guid Authenticate(User userIn)
        {
            User userOut = UserService.Get(userIn);
            if (userOut is null)
            {
                throw new InvalidCredentialException("Invalid credentials");
            }
            if (!userOut.Password.Equals(userIn.Password))
            {
                throw new InvalidCredentialException("Invalid credentials");
            }
            Session session = new Session()
            {
                User = userOut
            };
            SessionService.Add(session);
            return session.Id;
        }

        public User? GetCurrentUser(Guid? token = null)
        {
            if (CurrentUser is not null) return CurrentUser;
            if (token is null) throw new ArgumentException("Authorization token was null");
            Session sessionIn = new Session()
            {
                Id = (Guid)token
            };
            Session session = SessionService.Get(sessionIn);
            if (session is not null) CurrentUser = session.User;
            return CurrentUser;
        }

        public void Logout()
        {
            Session session = new Session()
            {
                User = CurrentUser
            };
            SessionService.Delete(session);
            CurrentUser = null;
        }
    }
}
