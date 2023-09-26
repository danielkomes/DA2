using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public void Delete(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public Purchase Get(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
        }

        public void Update(Purchase entity)
        {
            throw new NotImplementedException();
        }
    }
}
