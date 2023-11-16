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


        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not PaymentMethodEntity) return false;
            PaymentMethodEntity other = obj as PaymentMethodEntity;
            return Id == other.Id;
        }
    }
}
