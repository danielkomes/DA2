using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

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
            //get user
            //if found, create and insert session in DB
            //else throw exception
            User userOut = UserService.Get(userIn);
            if (userOut is null)
            {
                throw new InvalidCredentialException("Invalid credentials");
            }
            Session session = new Session()
            {
                User = userOut
            };
            SessionService.Add(session);
            SessionService.Save();
            return session.Id;
        }

        public User? GetCurrentUser(Guid? token = null)
        {
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
            throw new NotImplementedException();
        }
    }
}
