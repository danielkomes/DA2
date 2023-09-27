using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
