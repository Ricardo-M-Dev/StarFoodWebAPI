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
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<ProductVariations> ProductVariations { get; set; }
        public DbSet<DishesProductVariations> DishesProductVariations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurants>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Dishes>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<ProductTypes>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Categories>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ProductVariations>()
                .HasKey(pv => pv.Id);

            modelBuilder.Entity<DishesProductVariations>()
                .HasKey(dp => new { dp.DishesId, dp.ProductVariationId });

            modelBuilder.Entity<Dishes>()
                .HasOne(d => d.Type)
                .WithMany()
                .HasForeignKey(d => d.ProductTypeId);

            modelBuilder.Entity<Dishes>()
                .HasOne(d => d.Category)
                .WithMany()
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<Categories>()
                .HasOne(c => c.Restaurant)
                .WithOne()
                .HasForeignKey<Categories>(c => c.RestaurantId);

            modelBuilder.Entity<ProductTypes>()
                .HasOne(pt => pt.Restaurant)
                .WithOne()
                .HasForeignKey<ProductTypes>(pt => pt.RestaurantId);

            modelBuilder.Entity<ProductVariations>()
                .HasOne(pv => pv.Restaurant)
                .WithOne()
                .HasForeignKey<ProductVariations>(pv => pv.RestaurantId);

            modelBuilder.Entity<DishesProductVariations>()
                .HasOne(dp => dp.Dishes)
                .WithMany(d => d.DishesProductVariations)
                .HasForeignKey(dp => dp.DishesId);

            modelBuilder.Entity<DishesProductVariations>()
                .HasOne(dp => dp.ProductVariation)
                .WithMany(pv => pv.DishesProductVariations)
                .HasForeignKey(dp => dp.ProductVariationId);
        }

    }
}
