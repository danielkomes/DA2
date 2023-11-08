using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

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
        public IActionResult DoPurchase([FromBody] IEnumerable<Guid> currentProducts)
        {
            ShoppingCart.GetCurrentProducts(currentProducts);
            ShoppingCart.DoPurchase();
            return Ok();
        }
    }
}
