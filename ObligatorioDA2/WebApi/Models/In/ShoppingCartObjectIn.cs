using WebApi.Models.Out;

namespace WebApi.Models.In
{
    public class ShoppingCartObjectIn
    {
        public IEnumerable<Guid>? CurrentProducts { get; set; }
        public ProductModelIn? ProductToAdd { get; set; }
    }
}
