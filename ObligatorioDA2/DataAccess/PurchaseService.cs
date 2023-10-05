using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class PurchaseService : IService<Purchase>
    {
        private readonly Context Context;
        private readonly DbSet<Purchase> Table;

        public PurchaseService(Context context)
        {
            Context = context;
            Table = context.Set<Purchase>();
        }

        public void Add(Purchase entity)
        {
            Table.Add(entity);
            Save();
        }

        public void Delete(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> FindByCondition(Expression<Func<Purchase, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Purchase Get(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetAll()
        {
            return Table
                .Include(p => p.User)
                .Include(p => p.Promotion)
                .ToList();

        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(Purchase entity)
        {
            throw new NotImplementedException();
        }
    }
}
