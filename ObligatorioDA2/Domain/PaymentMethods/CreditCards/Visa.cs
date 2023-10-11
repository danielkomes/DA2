using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.CreditCards
{
    public class Visa : CreditCard
    {
        public Visa(User user) : base(user)
        {
        }
    }
}
