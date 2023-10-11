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
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
