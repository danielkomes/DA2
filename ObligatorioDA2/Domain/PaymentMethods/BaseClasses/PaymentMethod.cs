namespace Domain.PaymentMethods.BaseClasses
{
    public abstract class PaymentMethod
    {
        public PaymentMethodEntity Entity { get; set; }
        public PaymentMethod(PaymentMethodEntity entity)
        {
            Entity = entity;
        }
    }
}
