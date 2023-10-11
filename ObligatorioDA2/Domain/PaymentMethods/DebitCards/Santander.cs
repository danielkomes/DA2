using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.DebitCards
{
    public class Santander : DebitCard
    {
        public Santander(User user) : base(user)
        {
        }
    }
}
