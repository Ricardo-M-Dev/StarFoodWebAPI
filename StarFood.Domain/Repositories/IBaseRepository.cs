using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;

namespace StarFood.Application.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<TEntity> entity);
        Task AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);
        void Remove(TEntity entity);
        void RemoveById(object id);
        void Edit(TEntity entity);
        TEntity? GetById(object id);
        ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] id);
        Restaurants? GetRestaurantById(int id);
        void Detache(TEntity entity);
        EntityState GetEntityState(TEntity entity);
    }
}
