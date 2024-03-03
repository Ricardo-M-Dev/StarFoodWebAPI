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
        public DbSet<ProductOrder> ProductsOrder { get; set; }
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

            modelBuilder.Entity<ProductOrder>()
                .HasKey(op => op.Id);

            modelBuilder.Entity<ProductImages>()
                .HasKey(op => op.Id);

            modelBuilder.Entity<Orders>()
            .Property(o => o.Status)
            .HasConversion<string>();

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Variations>()
                .HasOne(v => v.Products)
                .WithMany(p => p.Variations)
                .HasForeignKey(v => v.ProductId);

            modelBuilder.Entity<Categories>()
                .HasOne(c => c.Restaurant)
                .WithMany(r => r.Categories)
                .HasForeignKey(c => c.RestaurantId);

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Restaurant)
                .WithMany(r => r.Products)
                .HasForeignKey(p => p.RestaurantId);

            modelBuilder.Entity<Variations>()
                .HasOne(v => v.Restaurant)
                .WithMany(r => r.Variations)
                .HasForeignKey(v => v.RestaurantId);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId);
        }
    }
}
