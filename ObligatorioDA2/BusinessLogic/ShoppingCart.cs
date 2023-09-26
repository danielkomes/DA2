﻿using Domain;
using IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ShoppingCart : IShoppingCart
    {
        public User? User { get; set; }
        public IEnumerable<Product> ProductsChecked { get; set; }
        public PromotionAbstract PromotionApplied { get; set; }
        private readonly IShoppingCartDataAccessHelper DataAccessHelper;

        public ShoppingCart(IShoppingCartDataAccessHelper dataAccessHelper)
        {
            ProductsChecked = new List<Product>();
            DataAccessHelper = dataAccessHelper;
        }

        public void AddToCart(Product product)
        {
            //verificar el producto desde la BD
            //añadir al carrito

            bool valid = DataAccessHelper.VerifyProduct(product);
            if (!valid) { return; } //TODO: throw exception 
            ProductsChecked = ProductsChecked.Append(product);
            //TODO: GetTotalPrice();
        }

        public void DoPurchase()
        {
            //verificar que el user está loggeado y existe en la BD, si no lo está, dar error
            //verificar productos en la bd, si algo falla, dar error
            //crear objeto Purchase
            //insertar objeto Purchase en la BD

            DataAccessHelper.VerifyUser(User);
            DataAccessHelper.VerifyProducts(ProductsChecked);
            Purchase purchase = new Purchase(User, ProductsChecked, PromotionApplied?.PromotionEntity);
            DataAccessHelper.InsertPurchase(purchase);
        }

        public float GetTotalPrice()
        {
            //esta función se llama cada vez que se agrega o elimina un producto del carrito...
            //...así que no necesito verificar el producto, ya se hace en el Add
            //verificar promociones en la BD, si algo falla, dar error
            //aplicar promociones y mostrar el total

            float total = 0;

            foreach (Product product in ProductsChecked)
            {
                total += product.Price;
            }
            PromotionApplied = null;

            IEnumerable<PromotionAbstract> promotions = DataAccessHelper.GetPromotions();
            foreach (PromotionAbstract promotion in promotions)
            {
                PromotionResult result = promotion.GetTotal(ProductsChecked);
                if (result.Result < total) total = result.Result;
                PromotionApplied = promotion;
            }
            return total;
        }

        public void RemoveFromCart(Product product)
        {
            //no necesita verificar, se elimina siempre
            ProductsChecked = ProductsChecked.Where(p => p != product);
        }
    }
}