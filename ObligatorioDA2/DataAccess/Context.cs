using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Context : DbContext
    {
        public DbSet<User> Users{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchase{ get; set; }
        public DbSet<PromotionEntity> Promotions{ get; set; }

        public Context(DbContextOptions<Context> builder) : base(builder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            //modelBuilder.Entity<Product>().HasMany<object>(/*v => v.Id*/)/*.WithMany<Product>()*/; //para setear en la bd relaciones entre tablas
            
            modelBuilder.Entity<Purchase>()
                .HasKey(p => p.Id); 
            modelBuilder.Entity<PromotionEntity>()
                .HasKey(p => p.Id);
        }
    }
}
