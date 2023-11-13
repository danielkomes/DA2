using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json.Nodes;

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
            //string p = "\"[";
            //foreach (Product product in entity.Products)
            //{
            //    p += $"\"{product.Id}\", ";
            //    p += $"\"{product.Name}\", ";
            //    p += $"\"{product.Price}\", ";
            //    p += $"\"{product.Description}\", ";
            //    p += $"\"{product.Brand}\", ";
            //    p += $"\"{product.Category}\", ";

            //    p += $"\"[\"red\"]\"";
            //    foreach (string c in product.Colors)
            //    {

            //    }
            //}
            //p += "]\"";
            //FormattableString sql = $"INSERT INTO Purchases (Id, UserId, Products, PromotionId, PaymentMethodId, Total, Date) VALUES ({entity.Id}, {entity.User.Id}, {p}, {entity.Promotion?.Id}, {entity.PaymentMethod.Id}, {entity.Total}, {entity.Date})";
            //Context.Database.ExecuteSqlInterpolated(sql);
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
