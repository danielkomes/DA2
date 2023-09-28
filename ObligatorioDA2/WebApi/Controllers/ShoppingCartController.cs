using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    [AuthorizationFilter(RoleNeeded = EUserRole.Customer)]
    [ExceptionFilter]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart ShoppingCart;
        public ShoppingCartController(IShoppingCart shoppingCart)
        {
            ShoppingCart = shoppingCart;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            //200 ok (o 204 no content)
            IEnumerable<ProductModelOut> models = new List<ProductModelOut>();
            foreach (Product p in ShoppingCart.ProductsChecked)
            {
                models = models.Append(new ProductModelOut(p));
            }
            return Ok(models);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductModelIn product)
        {
            ShoppingCart.AddToCart(product.ToEntity());
            return Ok("Product added to cart");
        }

        //remove product from cart
        [HttpDelete]
        public IActionResult RemoveSelectedProducts([FromBody] IEnumerable<ProductModelIn> products)
        {
            //200 ok
            foreach (ProductModelIn model in products)
            {
                ShoppingCart.RemoveFromCart(model.ToEntity());
            }
            return Ok("Product(s) removed");
        }

        //remove all products from cart
        [HttpDelete]
        public IActionResult RemoveAllProducts()
        {
            //200 ok
            ShoppingCart.ProductsChecked = Enumerable.Empty<Product>();
            return Ok("All products removed");
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpPost]
        public IActionResult DoPurchase()
        {
            ShoppingCart.DoPurchase();
            return Ok("Purchase done");
        }
    }
}
