using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.CreditCards
{
    public class Visa : CreditCard
    {
        public Visa(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
