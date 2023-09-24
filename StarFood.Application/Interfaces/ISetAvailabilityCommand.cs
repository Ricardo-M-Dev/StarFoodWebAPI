namespace StarFood.Application.Interfaces
{
    public interface ISetAvailabilityCommand<T>
    {
        Guid Id { get; set; }
        bool IsAvailable { get; set; }
    }

}
