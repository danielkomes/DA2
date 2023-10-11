using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PaymentMethods.BaseClasses
{
    public abstract class PaymentMethod
    {
        private readonly Guid Id;
        private readonly User User;
        public PaymentMethod(User user)
        {
            Id = Guid.NewGuid();
            User = user;
        }
    }
}
