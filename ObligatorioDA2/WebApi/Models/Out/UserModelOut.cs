using Domain;

namespace WebApi.Models.Out
{
    public class UserModelOut
    {
        public string Email { get; set; }
        public string Address { get; set; }

        public UserModelOut(User user)
        {
            Email = user.Email;
            Address = user.Address;
        }
    }
}
