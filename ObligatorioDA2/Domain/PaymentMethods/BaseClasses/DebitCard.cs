using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PaymentMethods.BaseClasses
{
    public abstract class DebitCard : PaymentMethod
    {
        protected DebitCard(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
