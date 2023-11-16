using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.DebitCards
{
    public class Santander : DebitCard
    {
        public Santander(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
