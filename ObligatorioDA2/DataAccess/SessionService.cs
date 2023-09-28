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
    public class SessionService : IService<Session>
    {
        private readonly Context Context;
        private readonly DbSet<Session> Table;

        public SessionService(Context context)
        {
            Context = context;
            Table = Context.Set<Session>();
        }

        public void Add(Session entity)
        {
            Table.Add(entity);
            Save();
            //throw new NotImplementedException();
        }

        public void Delete(Session entity)
        {
            var result = Table.FromSqlInterpolated($"SELECT * FROM Sessions")
                .Where(s => s.User.Equals(entity.User));
            Table.Remove(result.First());
            Save();
            //throw new NotImplementedException();
        }

        public bool Exists(Session entity)
        {
            return Get(entity) is not null;
            //throw new NotImplementedException();
        }

        public IEnumerable<Session> FindByCondition(Expression<Func<Session, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Session Get(Session entity)
        {
            //return Table.Select(u => u.Id.Equals(entity.Id)) as Session;
            var ret = Table.FromSqlInterpolated($"SELECT * FROM Sessions")
                .Where(s => s.Id.Equals(entity.Id));
            if (!ret.Any()) return null;
            return ret.First();
        }

        public IEnumerable<Session> GetAll()
        {
            var ret = Table.FromSqlInterpolated($"SELECT * FROM Sessions");
            return ret.ToList();
            //throw new NotImplementedException();
        }

        public void Save()
        {
            Context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Update(Session entity)
        {
            //Table.Update(entity);
            //throw new NotImplementedException();
        }
    }
}
