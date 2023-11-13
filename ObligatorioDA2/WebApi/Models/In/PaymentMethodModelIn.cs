using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;
using Domain.PaymentMethods.CreditCards;

namespace WebApi.Models.In
{
    public class PaymentMethodModelIn
    {
        public EPaymentMethodType Type { get; set; }
    }
}
