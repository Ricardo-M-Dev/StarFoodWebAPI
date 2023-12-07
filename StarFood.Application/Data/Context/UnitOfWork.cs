using StarFood.Application.Base;

namespace StarFood.Application.Data.Context
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : IContext
    {
        private readonly T _context;

        public UnitOfWork(T context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
