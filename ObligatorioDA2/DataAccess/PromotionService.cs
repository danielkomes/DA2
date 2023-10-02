using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class PromotionService : IService<PromotionEntity>
    {
        private readonly Context Context;
        private readonly DbSet<PromotionEntity> Table;
        public PromotionService(Context context)
        {
            Context = context;
            Table = context.Set<PromotionEntity>();
        }
        public void Add(PromotionEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(PromotionEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(PromotionEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PromotionEntity> FindByCondition(Expression<Func<PromotionEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public PromotionEntity Get(PromotionEntity entity)
        {
            return null;
        }

        public IEnumerable<PromotionEntity> GetAll()
        {
            return Table.ToList();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(PromotionEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
