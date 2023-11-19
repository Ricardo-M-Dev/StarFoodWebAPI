using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StarFoodDbContext _context;

        public UserRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Users> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>u.Username == username);
            return user;
        }

        public async Task UpdateAsync(int id, Users updateUser)
        {
            _context.Users.Update(updateUser);
            await _context.SaveChangesAsync();
        }
    }
}
