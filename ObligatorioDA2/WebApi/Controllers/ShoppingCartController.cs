﻿using Domain;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.In;
using WebApi.Models.Out;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
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
            return Ok("");
        }

        //remove product from cart
        [HttpDelete]
        public IActionResult RemoveSelectedProducts([FromQuery] IEnumerable<string> id)
        {
            //200 ok
            return Ok("Product(s) removed");
        }

        //remove all products from cart
        [HttpDelete]
        public IActionResult RemoveAllProducts()
        {
            //200 ok
            return Ok("All products removed");
        }
    }
}