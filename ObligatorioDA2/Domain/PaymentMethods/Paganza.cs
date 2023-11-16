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
