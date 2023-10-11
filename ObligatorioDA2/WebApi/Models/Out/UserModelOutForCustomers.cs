using Domain;

namespace WebApi.Models.Out
{
    public class UserModelOutForCustomers
    {
        public string Email { get; set; }
        public string Address { get; set; }

        public UserModelOutForCustomers(User user)
        {
            Email = user.Email;
            Address = user.Address;
        }
    }
}
