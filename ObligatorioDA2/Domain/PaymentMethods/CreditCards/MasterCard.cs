using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.CreditCards
{
    public class MasterCard : CreditCard
    {
        public MasterCard(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
