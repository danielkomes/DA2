using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        }

        public void Delete(Session entity)
        {
            var result = Table.FromSqlInterpolated($"SELECT * FROM Sessions")
                .Where(s => s.User.Equals(entity.User));
            Table.Remove(result.First());
            Save();
        }

        public bool Exists(Session entity)
        {
            return Get(entity) is not null;
        }

        public IEnumerable<Session> FindByCondition(Expression<Func<Session, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Session Get(Session entity)
        {
            var ret = Table
                .Include(s => s.User)
                .Where(s => s.Id.Equals(entity.Id));
            if (!ret.Any()) return null;
            return ret.First();
        }

        public IEnumerable<Session> GetAll()
        {
            var ret = Table.FromSqlInterpolated($"SELECT * FROM Sessions");
            return ret.ToList();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(Session entity)
        {
            throw new NotImplementedException();
        }
    }
}
