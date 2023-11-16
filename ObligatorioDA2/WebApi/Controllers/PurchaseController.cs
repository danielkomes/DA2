using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;

namespace WebApi.Controllers
{
    [Route("api/purchases")]
    [ApiController]
    [ExceptionFilter]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseLogic PurchaseLogic;
        private readonly IShoppingCart ShoppingCart;
        public PurchaseController(IPurchaseLogic purchaseLogic, IShoppingCart shoppingCart)
        {
            PurchaseLogic = purchaseLogic;
            ShoppingCart = shoppingCart;
        }

        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Purchase> purchases = PurchaseLogic.GetAll();
            return Ok(purchases);
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [AuthorizationFilter(RoleNeeded = EUserRole.Customer)]
        [HttpPost]
        public IActionResult DoPurchase([FromBody] ShoppingCartModelIn model)
        {
            IEnumerable<Guid> currentProducts = model.Products;
            EPaymentMethodType paymentMethod = model.PaymentMethod.Type;
            ShoppingCart.PaymentMethod = paymentMethod;
            ShoppingCart.GetCurrentProducts(currentProducts);
            ShoppingCart.DoPurchase();
            return Ok();
        }
    }
}
