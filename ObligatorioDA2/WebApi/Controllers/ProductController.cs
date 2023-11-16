using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [ExceptionFilter]
    public class ProductController : ControllerBase
    {
        private readonly IProductLogic ProductLogic;
        public ProductController(IProductLogic productLogic)
        {
            ProductLogic = productLogic;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] ProductFilterModelIn? filter = null)
        {
            string name = null;
            string brand = null;
            string category = null;
            if (filter != null)
            {
                name = filter.Name;
                brand = filter.Brand;
                category = filter.Category;
            }
            IEnumerable<Product> products = ProductLogic.FindByCondition(name, brand, category);
            IEnumerable<ProductModelOut> models = new List<ProductModelOut>();
            foreach (Product product in products)
            {
                models = models.Append(new ProductModelOut(product));
            }
            return Ok(models);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            Product result = ProductLogic.Get(id);
            return Ok(new ProductModelOut(result));
        }
    }
}
