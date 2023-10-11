using Domain;

namespace WebApi.Models.Out
{
    public class UserModelOutForAdmins
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public IEnumerable<EUserRole> Roles { get; set; }

        public UserModelOutForAdmins(User user)
        {
            Email = user.Email;
            Address = user.Address;
            Roles = user.Roles;
        }
    }
}
