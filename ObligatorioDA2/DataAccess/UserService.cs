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
            throw new NotImplementedException();
            //Table.Add(entity);
            //Save();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch(DbUpdateException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
