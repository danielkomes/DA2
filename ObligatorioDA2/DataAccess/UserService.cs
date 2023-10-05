using DataAccess.Exceptions;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        }

        public void Delete(User entity)
        {
            Table.Remove(Get(entity));
            Save();
        }

        public bool Exists(User entity)
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

        public IEnumerable<User> FindByCondition(Expression<Func<User, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public User Get(User entity)
        {
            if (entity is null) throw new ResourceNotFoundException("User not found");
            var ret = Table.FirstOrDefault(u => u.Email.Equals(entity.Email));
            if (ret is null) throw new ResourceNotFoundException("User not found");
            return ret;
        }

        public IEnumerable<User> GetAll()
        {
            var ret = Table.FromSqlInterpolated($"SELECT * FROM Users");
            return ret.ToList();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(User entity)
        {
            var found = Table.FirstOrDefault(u => u.Id == entity.Id);
            if (found is null) throw new ResourceNotFoundException("User not found");
            found.Email = entity.Email;
            found.Address = entity.Address;
            Save();
        }
    }
}
