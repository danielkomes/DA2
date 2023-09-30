using Domain;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace WebApi.Models.In
{
    public class UserModelIn
    {
        public string Email { get; set; }
        public string Address { get; set; }

        public User ToEntity()
        {
            return new User()
            {
                Email = Email,
                Address = Address
            };
        }
    }
}
