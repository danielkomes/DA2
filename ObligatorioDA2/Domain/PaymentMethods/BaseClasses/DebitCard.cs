namespace Domain.PaymentMethods.BaseClasses
{
    public abstract class DebitCard : PaymentMethod
    {
        protected DebitCard(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
