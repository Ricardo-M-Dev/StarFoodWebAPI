using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data
{
    public class StarFoodDbContext : DbContext
    {
        public StarFoodDbContext(DbContextOptions<StarFoodDbContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurants> Restaurants { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<ProductCategories> Categories { get; set; }
        public DbSet<Variations> Variations { get; set; }
        public DbSet<ProductVariations> ProductVariations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurants>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Products>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<ProductTypes>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProductCategories>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Variations>()
                .HasKey(pv => pv.Id);

            modelBuilder.Entity<ProductVariations>()
                .HasKey(dp => new { dp.ProductId, dp.VariationId });

            modelBuilder.Entity<Products>()
                .HasOne(d => d.Type)
                .WithMany()
                .HasForeignKey(d => d.ProductTypeId);

            modelBuilder.Entity<Products>()
                .HasOne(d => d.Category)
                .WithMany()
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<ProductCategories>()
                .HasOne(c => c.Restaurant)
                .WithOne()
                .HasForeignKey<ProductCategories>(c => c.RestaurantId);

            modelBuilder.Entity<ProductTypes>()
                .HasOne(pt => pt.Restaurant)
                .WithOne()
                .HasForeignKey<ProductTypes>(pt => pt.RestaurantId);

            modelBuilder.Entity<Variations>()
                .HasOne(pv => pv.Restaurant)
                .WithOne()
                .HasForeignKey<Variations>(pv => pv.RestaurantId);

            modelBuilder.Entity<ProductVariations>()
                .HasOne(dp => dp.Product)
                .WithMany(d => d.ProductsProductVariations)
                .HasForeignKey(dp => dp.ProductId);

            modelBuilder.Entity<ProductVariations>()
                .HasOne(dp => dp.Variation)
                .WithMany(pv => pv.ProductsProductVariations)
                .HasForeignKey(dp => dp.VariationId);
        }

    }
}
