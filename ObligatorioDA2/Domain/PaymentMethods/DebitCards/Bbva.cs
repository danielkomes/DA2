using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.DebitCards
{
    public class Bbva : DebitCard
    {
        public Bbva(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
