using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IDishesProductVariationsRepository
    {
        Task<List<DishesProductVariations>> GetAllAsync(int restaurantId);
        Task<DishesProductVariations> GetByIdAsync(int id);
        Task<List<DishesProductVariations>> GetByDishId(int dishId);
        Task<List<DishesProductVariations>> GetByProductVariationId(int productVariationId);
        Task CreateAsync(DishesProductVariations dishesProductVariations);
        Task UpdateAsync(int id, DishesProductVariations dishesProductVariations);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
