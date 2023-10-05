using Domain;

namespace IBusinessLogic
{
    public interface ISessionLogic
    {
        User? GetCurrentUser(Guid? token = null);
        Guid Authenticate(User user);
        void Logout();
    }
}
