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
        public Guid Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public User? GetCurrentUser(Guid? token = null)
        {
            throw new NotImplementedException();
        }
    }
}
