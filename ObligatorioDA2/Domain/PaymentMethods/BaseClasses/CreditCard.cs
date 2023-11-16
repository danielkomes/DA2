namespace Domain.PaymentMethods.BaseClasses
{
    public abstract class CreditCard : PaymentMethod
    {
        protected CreditCard(PaymentMethodEntity entity) : base(entity)
        {
        }
    }
}
