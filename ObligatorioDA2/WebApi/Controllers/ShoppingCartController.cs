using Domain;
using Domain.PaymentMethods;
using Domain.PaymentMethods.BaseClasses;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
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
        public IActionResult CalculateTotal([FromBody] ShoppingCartModelIn model)
        {
            IEnumerable<Guid> currentProducts = model.Products;
            EPaymentMethodType paymentMethod = model.PaymentMethod.Type;

            IEnumerable<ProductModelOut> models = new List<ProductModelOut>();
            ShoppingCart.GetCurrentProducts(currentProducts);
            foreach (Product p in ShoppingCart.ProductsChecked)
            {
                models = models.Append(new ProductModelOut(p));
            }
            ShoppingCart.PaymentMethod = paymentMethod;
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
    }
}
