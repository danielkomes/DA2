using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PaymentMethods.BaseClasses
{
    public abstract class CreditCard : PaymentMethod
    {
        protected CreditCard(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
