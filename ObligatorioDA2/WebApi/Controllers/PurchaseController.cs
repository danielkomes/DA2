using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        //get all
        [AuthorizationFilter(RoleNeeded = EUserRole.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Purchase> purchases = PurchaseService.GetAll();
            return Ok(purchases);
        }
    }
}
