using Microsoft.EntityFrameworkCore;
using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Domain.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private DbContext _context { get; set; }
        protected DbSet<TEntity> DbSet;
        protected DbSet<Restaurants> Stations;

        public BaseRepository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
            Stations = _context.Set<Restaurants>();
        }

        public Restaurants? GetRestaurantById(int id)
        {
            return this.Stations.FirstOrDefault(x => x.RestaurantId == id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var added = await DbSet.AddAsync(entity, cancellationToken);
            return;
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddRangeAsync(entity, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveById(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.DbSet.Remove(entity);
            }
        }

        public void Edit(TEntity entity)
        {
            this.DbSet.Update(entity);
        }

        public virtual TEntity? GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public async ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] id)
        {
            TEntity? item = await this.DbSet.FindAsync(id, cancellationToken);
            return item;
        }

        public void Detache(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public EntityState GetEntityState(TEntity entity)
        {
            return _context.Entry(entity).State;
        }
    }
}
