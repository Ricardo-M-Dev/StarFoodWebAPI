using Microsoft.EntityFrameworkCore;
using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data
{
    public class StarFoodDbContext : DbContext, IContext
    {
        public StarFoodDbContext(DbContextOptions<StarFoodDbContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurants> Restaurants { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Variations> Variations { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Tables> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurants>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Products>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Categories>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Variations>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Orders>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Tables>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Variations>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variations)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => op.Id);

            modelBuilder.Entity<OrderProducts>()
            .HasOne(op => op.Orders)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProducts>()
            .HasOne(op => op.Products)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);
        }
    }
}
