using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(Users user);
        Task<Users> GetByIdAsync(int id);
        Task UpdateAsync(int id, Users user);
        Task DeleteAsync(int id);
    }
}
