namespace StarFood.Application.Interfaces
{
    public interface ICommandHandler<TCommand, TResult>
    {
        Task<TResult> HandleAsync(TCommand command, int restaurantId);
        Task<List<TResult>> HandleAsyncList(List<TCommand> commandList, int restaurantId);
    }
}
