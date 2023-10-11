using Domain;
using IBusinessLogic;
using IDataAccess;
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
        public PurchaseController(IPurchaseLogic purchaseLogic)
        {
            PurchaseLogic = purchaseLogic;
        }

        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Purchase> purchases = PurchaseLogic.GetAll();
            return Ok(purchases);
        }
    }
}
