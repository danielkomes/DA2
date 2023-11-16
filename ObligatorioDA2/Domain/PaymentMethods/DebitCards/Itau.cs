using Domain.PaymentMethods.BaseClasses;

namespace Domain.PaymentMethods.DebitCards
{
    public class Itau : DebitCard
    {
        public Itau(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
