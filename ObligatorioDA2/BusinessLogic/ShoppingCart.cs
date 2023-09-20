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

        public void AddToCart(Product product)
        {
            throw new NotImplementedException();
        }

        public void DoPurchase()
        {
            throw new NotImplementedException();
        }

        public float GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
