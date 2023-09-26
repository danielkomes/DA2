using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLogic
{
    public interface ISessionLogic
    {
        User? GetCurrentUser(Guid? token = null);
        Guid Authenticate(string email, string password);
    }
}
