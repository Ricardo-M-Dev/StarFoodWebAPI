using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface ITablesRepository : IBaseRepository<Tables>
    {
        List<Tables> GetByRestaurantId(int restaurantId);
        Tables GetTableById(int tableId, int restaurantId);
        Tables GetByBarcode(string tableBarcode, int restaurantId);
        Tables GetByNumber(int number, int restaurantId);
    }
}
