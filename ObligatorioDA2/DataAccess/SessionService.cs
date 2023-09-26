using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SessionService : IService<Session>
    {
        private readonly Context Context;
        private readonly DbSet<Session> Table;

        public void Add(Session entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Session entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Session entity)
        {
            throw new NotImplementedException();
        }

        public Session Get(Session entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Session> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Session entity)
        {
            throw new NotImplementedException();
        }
    }
}
