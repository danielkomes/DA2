﻿using Domain;

namespace WebApi.Models.In
{
    public class UserModelInForAdmins
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public IEnumerable<EUserRole> Roles { get; set; }

        public User ToEntity()
        {
            return new User()
            {
                Email = Email,
                Address = Address,
                Roles = Roles
            };
        }
    }
}
