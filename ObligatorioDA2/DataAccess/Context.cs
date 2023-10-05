using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace DataAccess
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PromotionEntity> Promotions { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public Context() { }
        public Context(DbContextOptions builder) : base(builder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(p => p.Roles)
                .HasConversion
                (
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<IEnumerable<EUserRole>>(v, (JsonSerializerOptions)null),
                    new ValueComparer<IEnumerable<EUserRole>>
                    (
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Colors)
                .HasConversion
                (
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<IEnumerable<string>>(v, (JsonSerializerOptions)null),
                    new ValueComparer<IEnumerable<string>>
                    (
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );

            modelBuilder.Entity<PromotionEntity>()
                .HasKey(p => p.Id);


            modelBuilder.Entity<Purchase>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Purchase>().Property(p => p.Products)
                .HasConversion
                (
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<IEnumerable<Product>>(v, (JsonSerializerOptions)null),
                    new ValueComparer<IEnumerable<Product>>
                    (
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );

            modelBuilder.Entity<Session>()
                .HasKey(p => p.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(directory)
         .AddJsonFile("appsettings.json")
         .Build();

                var connectionString = configuration.GetConnectionString(@"obligatorioDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
