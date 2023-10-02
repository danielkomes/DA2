using Domain;
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
        private readonly IService<Purchase> PurchaseService;
        public PurchaseController(IService<Purchase> purchaseService)
        {
            PurchaseService = purchaseService;
        }

        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Purchase> purchases = PurchaseService.GetAll();
            return Ok(purchases);
        }
    }
}
