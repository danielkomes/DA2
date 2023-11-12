using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.Out;

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

        [HttpPost]
        public IActionResult CalculateTotal([FromBody] IEnumerable<Guid> currentProducts)
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

        // [HttpPost("{productToAdd}")]
        // public IActionResult AddProduct([FromRoute] Guid productToAdd, [FromBody] IEnumerable<Guid> currentProducts)
        // {
        //     ShoppingCart.GetCurrentProducts(currentProducts);
        //     ShoppingCart.AddToCart(productToAdd);

        //     return Ok(GenerateResponseBody("Product added to cart"));
        // }

        // [HttpDelete("{productToRemove}")]
        // public IActionResult RemoveSelectedProduct([FromRoute] Guid productToRemove, [FromBody] IEnumerable<Guid> currentProducts)
        // {
        //     ShoppingCart.GetCurrentProducts(currentProducts);
        //     ShoppingCart.RemoveFromCart(productToRemove);


        //     return Ok(GenerateResponseBody("Product removed from cart"));
        // }

        // [HttpDelete]
        // public IActionResult RemoveAllProducts()
        // {
        //     ShoppingCart.ProductsChecked = Enumerable.Empty<Product>();
        //     return Ok("All products removed");
        // }

        // [ServiceFilter(typeof(AuthenticationFilter))]
        // [AuthorizationFilter(RoleNeeded = EUserRole.Customer)]
        // [HttpPost("purchase")]
        // public IActionResult DoPurchase([FromBody] IEnumerable<Guid> currentProducts)
        // {
        //     ShoppingCart.GetCurrentProducts(currentProducts);
        //     ShoppingCart.DoPurchase();
        //     return Ok(GenerateResponseBody("Purchase done"));
        // }

        private dynamic GenerateResponseBody(string result)
        {
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
                result = result,
                promotionApplied = promotionApplied,
                totalPrice = total,
                currentProducts = ids
            };
            return ret;
        }
    }
}
