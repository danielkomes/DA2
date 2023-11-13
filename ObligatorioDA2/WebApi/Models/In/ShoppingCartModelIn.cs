using Domain.PaymentMethods.BaseClasses;

namespace WebApi.Models.In
{
    public class ShoppingCartModelIn
    {
        public IEnumerable<Guid> Products { get; set; }
        public PaymentMethodModelIn PaymentMethod { get; set; }
    }
}
