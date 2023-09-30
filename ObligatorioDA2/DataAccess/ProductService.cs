using DataAccess.Exceptions;
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
            try
            {
                return Get(entity) is not null;
            }
            catch (ResourceNotFoundException)
            {
                return false;
            }
        }

        public IEnumerable<Product> FindByCondition(Expression<Func<Product, bool>> condition)
        {
            var ret = Table
                .Where(condition);
            return ret;
        }

        public Product Get(Product entity)
        {
            var ret = Table
                .Where(s => s.Id.Equals(entity.Id));
            if (!ret.Any()) throw new ResourceNotFoundException("Product not found");
            return ret.First();
        }

        public IEnumerable<Product> GetAll()
        {
            var ret = Table.FromSqlInterpolated($"SELECT * FROM Products");
            return ret.ToList();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
