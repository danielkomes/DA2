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
