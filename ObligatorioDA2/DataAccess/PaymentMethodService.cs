using DataAccess.Exceptions;
using Domain.PaymentMethods;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class PaymentMethodService : IService<PaymentMethodEntity>
    {
        private readonly Context Context;
        private readonly DbSet<PaymentMethodEntity> Table;

        public PaymentMethodService(Context context)
        {
            Context = context;
            Table = context.Set<PaymentMethodEntity>();
        }

        public void Add(PaymentMethodEntity entity)
        {
            Table.Add(entity);

            //Table.FromSqlInterpolated($"INSERT INTO PaymentMethods (Id, Type, UserId) VALLUES ({entity.Id}, {entity.Type}, {entity.User.Id})");
            Save();
        }

        public void Delete(PaymentMethodEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(PaymentMethodEntity entity)
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

        public IEnumerable<PaymentMethodEntity> FindByCondition(Expression<Func<PaymentMethodEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public PaymentMethodEntity Get(PaymentMethodEntity entity)
        {
            //var paymentMethod = Table.Where(y => y.User.Equals(entity.User)).FirstOrDefault();
            //var paymentMethod = Table.Include(y => y.User).ToList();
            //var paymentMethod = Table.FirstOrDefault(y => y.User.Equals(entity.User));
            var ret = Table.FromSqlInterpolated($"SELECT * FROM PaymentMethods WHERE UserId = {entity.User.Id} AND Type = {entity.Type}");

            if (ret == null || ret.Count() == 0)
            {
                throw new ResourceNotFoundException("Payment method not found");
            }
            return ret.First();
        }

        public IEnumerable<PaymentMethodEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(PaymentMethodEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
