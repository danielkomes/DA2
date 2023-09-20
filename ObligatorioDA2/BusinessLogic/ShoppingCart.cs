using Domain;
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
        public User User { get; set; }
        public IEnumerable<Product> ProductsChecked { get; set; }
        public IShoppingCartDataAccessHelper DataAccessHelper { get; set; }

        public void AddToCart(Product product)
        {
            //verificar el producto desde la BD
            //añadir al carrito
            throw new NotImplementedException();
        }

        public void DoPurchase()
        {
            //verificar que el user está loggeado y existe en la BD, si no lo está, dar error
            //verificar productos en la bd, si algo falla, dar error
            //crear objeto Purchase
            //insertar objeto Purchase en la BD
            throw new NotImplementedException();
        }

        public float GetTotalPrice()
        {
            //esta función se llama cada vez que se agrega o elimina un producto del carrito...
            //...así que no necesito verificar el producto, ya se hace en el Add
            //verificar promociones en la BD, si algo falla, dar error
            //aplicar promociones y mostrar el total
            throw new NotImplementedException();
        }

        public void RemoveFromCart(Product product)
        {
            //no necesita verificar, se elimina siempre
            throw new NotImplementedException();
        }
    }
}
