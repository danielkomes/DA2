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
                p => p.Name.Equals(name) ||
                p.Brand.Equals(brand) ||
                p.Category.Equals(category));
            }
            IEnumerable<ProductModelOut> models = new List<ProductModelOut>();
            foreach (Product product in products)
            {
                models = models.Append(new ProductModelOut(product));
            }
            return Ok(models);
        }

        [HttpGet]
        public IActionResult Get([FromBody] ProductModelIn model)
        {
            Product p = ProductService.Get(model.ToEntity());
            return Ok(new ProductModelOut(p));
        }
    }
}
