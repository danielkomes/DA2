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
                result = "Product removed from cart",
                promotionApplied = promotionApplied,
                totalPrice = total,
                currentProducts = ids
            };
            return Ok(ret);
        }

        [HttpDelete]
        public IActionResult RemoveAllProducts()
        {
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
