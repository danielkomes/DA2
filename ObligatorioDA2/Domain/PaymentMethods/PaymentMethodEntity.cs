namespace Domain.PaymentMethods
{
    public class PaymentMethodEntity
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public EPaymentMethodType Type { get; set; }

        public PaymentMethodEntity(User user, EPaymentMethodType type)
        {
            Id = new Guid();
            User = user;
            Type = type;
        }

        private PaymentMethodEntity() { }
    }
}
