using StarFood.Application.Interfaces;
using StarFood.Domain.Base;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class TablesRepository : BaseRepository<Tables>, ITablesRepository
    {
        public TablesRepository(StarFoodDbContext context) : base(context) 
        { 

        }

        public Tables GetByBarcode(string tableBarcode, int restaurantId)
        {
            Tables? table = base.DbSet
                .Where(t => t.Barcode == tableBarcode && t.RestaurantId == restaurantId)
                .FirstOrDefault();

            return table;
        }

        public List<Tables> GetByRestaurantId(int restaurantId)
        {
            List<Tables> tables = base.DbSet
                .Where(t => t.RestaurantId == restaurantId)
                .ToList();

            return tables;
        }

        public Tables GetTableById(int tableId, int restaurantId)
        {
            Tables? table = base.DbSet
                .Where(t => t.Id == tableId && t.RestaurantId == restaurantId)
                .FirstOrDefault();

            return table;
        }

        public Tables GetByNumber(int number, int restaurantId)
        {
            Tables? table = base.DbSet
                .Where(t => t.Number == number && t.RestaurantId == restaurantId)
                .FirstOrDefault();

            return table;
        }
    }
}
