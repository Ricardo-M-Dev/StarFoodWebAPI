namespace StarFood.Application.Base
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface IUnitOfWork<T> : IUnitOfWork
    {
    }
}
