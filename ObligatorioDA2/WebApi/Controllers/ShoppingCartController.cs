using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    [ExceptionFilter]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart ShoppingCart;
        public ShoppingCartController(IShoppingCart shoppingCart)
        {
            ShoppingCart = shoppingCart;
        }

        [HttpGet]
        public IActionResult GetProducts([FromBody] IEnumerable<Guid> currentProducts)
        {
            IEnumerable<ProductModelOut> models = new List<ProductModelOut>();
            ShoppingCart.GetCurrentProducts(currentProducts);
            foreach (Product p in ShoppingCart.ProductsChecked)
            {
                models = models.Append(new ProductModelOut(p));
            }
            float total = ShoppingCart.GetTotalPrice();
            string promotionApplied = ShoppingCart.PromotionApplied?.PromotionEntity.Name;
            if (promotionApplied is null) promotionApplied = "None";
            var ret = new
            {
                checkedProducts = models,
                promotionApplied = promotionApplied,
                totalPrice = total
            };
            return Ok(ret);
        }

        [HttpPost("{productToAdd}")]
        public IActionResult AddProduct([FromRoute] Guid productToAdd, [FromBody] IEnumerable<Guid> currentProducts)
        {
            ShoppingCart.GetCurrentProducts(currentProducts);
            Product newProduct = new Product()
            {
                Id = productToAdd
            };
            ShoppingCart.AddToCart(newProduct);
            float total = ShoppingCart.GetTotalPrice();
            List<Guid> ids = new List<Guid>();
            foreach (Product product in ShoppingCart.ProductsChecked)
            {
                ids.Add(product.Id);
            }
            string promotionApplied = ShoppingCart.PromotionApplied?.PromotionEntity.Name;
            if (promotionApplied is null) promotionApplied = "None";
            var ret = new
            {
                result = "Product added to cart",
                promotionApplied = promotionApplied,
                totalPrice = total,
                currentProducts = ids
            };
            return Ok(ret);
        }

        //remove product from cart
        [HttpDelete("{id}")]
        public IActionResult RemoveSelectedProduct([FromRoute] Guid id, [FromBody] IEnumerable<Guid> currentProducts)
        {
            ShoppingCart.GetCurrentProducts(currentProducts);
            Product p = new Product()
            {
                Id = id
            };
            ShoppingCart.RemoveFromCart(p);
            float total = ShoppingCart.GetTotalPrice();
            List<Guid> ids = new List<Guid>();
            foreach (Product product in ShoppingCart.ProductsChecked)
            {
                ids.Add(product.Id);
            }
            string promotionApplied = ShoppingCart.PromotionApplied?.PromotionEntity.Name;
            if (promotionApplied is null) promotionApplied = "None";
            var ret = new
            {
                result = "Product added to cart",
                promotionApplied = promotionApplied,
                totalPrice = total,
                currentProducts = ids
            };
            return Ok(ret);
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
        [AuthorizationFilter(RoleNeeded = EUserRole.Customer)]
        [HttpPost]
        public IActionResult DoPurchase([FromBody] IEnumerable<Guid> currentProducts)
        {
            ShoppingCart.GetCurrentProducts(currentProducts);
            ShoppingCart.DoPurchase();
            return Ok("Purchase done");
        }
    }
}
