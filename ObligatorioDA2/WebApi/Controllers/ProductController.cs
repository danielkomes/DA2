using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> ProductService;
        public ProductController(IService<Product> productService)
        {
            ProductService = productService;
        }

        //get all
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("");
        }
    }
}
