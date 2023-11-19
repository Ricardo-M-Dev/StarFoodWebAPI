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
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Variations> Variations { get; set; }
        public DbSet<Users> Users { get; set; }

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

            modelBuilder.Entity<Users>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Products>()
                .HasOne(d => d.Category)
                .WithMany()
                .HasForeignKey(d => d.CategoryId);
        }

    }
}
