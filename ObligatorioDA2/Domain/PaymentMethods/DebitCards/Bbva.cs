using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.DebitCards
{
    public class Bbva : DebitCard
    {
        public Bbva(User user) : base(user)
        {
        }
    }
}
