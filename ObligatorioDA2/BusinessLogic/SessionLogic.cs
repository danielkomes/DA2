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
        //private readonly IService<Session> SessionService;
        private readonly IService<User> UserService;
        //public SessionLogic(IService<Session> sessionService, IService<User> userService)
        //{
        //    SessionService = sessionService;
        //    UserService = userService;
        //}
        public SessionLogic(IService<User> userService)
        {
            UserService = userService;
        }
        public bool Authenticate(User userIn)
        {
            //get user
            //if found, create and insert session in DB
            //else throw exception
            User userOut = UserService.Get(userIn);
            if (userOut is null)
            {
                return false;
            }
            //Session session = new Session()
            //{
            //    User = userOut
            //};
            //SessionService.Add(session);
            //return session.Id;
            return true;
        }

        public User? GetCurrentUser(Guid? token = null)
        {
            //if (CurrentUser is not null) return CurrentUser;
            ////if (token is null) throw new ArgumentException("Authorization token was null");
            //if (token is null) token = new Guid();
            //Session sessionIn = new Session()
            //{
            //    Id = (Guid)token
            //};
            //Session session = SessionService.Get(sessionIn);
            //if (session is not null) CurrentUser = session.User;
            return CurrentUser;
        }

        public void Logout()
        {
            //Session session = new Session()
            //{
            //    User = CurrentUser
            //};
            //SessionService.Delete(session);
            CurrentUser = null;
        }
    }
}
