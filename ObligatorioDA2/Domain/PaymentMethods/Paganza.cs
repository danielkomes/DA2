using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods
{
    public class Paganza : PaymentMethod
    {
        public Paganza(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
