using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserService : IService<User>
    {
        private readonly Context Context;
        private readonly DbSet<User> Table;

        public UserService(Context context)
        {
            Context = context;
            Table = context.Set<User>();
        }

        public void Add(User entity)
        {
            Table.Add(entity);
            Save();
            //throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            Table.Remove(Get(entity));
            Save();
            //throw new NotImplementedException();
        }

        public bool Exists(User entity)
        {
            return Get(entity) is not null;
            //throw new NotImplementedException();
        }

        public IEnumerable<User> FindByCondition(Expression<Func<User, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public User Get(User entity)
        {
            //return Table.Select(u => u.Email.Equals(entity.Email)) as User;
            var ret = Table.FromSqlInterpolated($"SELECT * FROM Users")
                .Where(u => u.Email.Equals(entity.Email));

            if (!ret.Any()) return null;
            return ret.First();
            //throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            var ret = Table.FromSqlInterpolated($"SELECT * FROM Sessions");
            return ret.ToList();
            //throw new NotImplementedException();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(User entity)
        {
            Table.Update(entity);
            Save();
            //throw new NotImplementedException();
        }
    }
}
