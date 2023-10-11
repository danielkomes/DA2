using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProductLogic : IProductLogic
    {
        private readonly IService<Product> ProductService;
        public ProductLogic(IService<Product> productService)
        {
            ProductService = productService;
        }

        public Product Get(Guid id)
        {
            Product product = new Product()
            {
                Id = id
            };
            return ProductService.Get(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return ProductService.GetAll();
        }
    }
}
