﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods
{
    public class Paypal : PaymentMethod
    {
        public Paypal(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
