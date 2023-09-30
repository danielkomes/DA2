using Domain;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [ExceptionFilter]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> ProductService;
        public ProductController(IService<Product> productService)
        {
            ProductService = productService;
        }

        //get all
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] string? name = null,
            [FromQuery] string? brand = null,
            [FromQuery] string? category = null)
        {
            //200 ok (o 204 no content)
            IEnumerable<Product> products;
            if (name is null && brand is null && category is null)
            {
                products = ProductService.GetAll();
            }
            else
            {
                products = ProductService.FindByCondition(
                p => p.Name.Contains(name) ||
                p.Brand.Contains(brand) ||
                p.Category.Contains(category));
            }
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
            Product p = new Product()
            {
                Id = id
            };
            Product result = ProductService.Get(p);
            if (result is null) return NotFound("Product not found");
            return Ok(new ProductModelOut(result));
        }
    }
}
