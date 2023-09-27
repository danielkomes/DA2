using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductService : IService<Product>
    {
        private readonly Context Context;
        private readonly DbSet<Product> Table;

        public ProductService(Context context)
        {
            Context = context;
            Table = context.Set<Product>();
        }
        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> FindByCondition(Expression<Func<Product, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Product Get(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
