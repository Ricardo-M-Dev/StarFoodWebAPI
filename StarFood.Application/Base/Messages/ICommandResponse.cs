namespace StarFood.Application.Base.Messages
{
    public interface ICommandResponse
    {
        bool IsValid { get; }
        Exception? Exception { get; set; }
        object? Object { get; set; }
    }
}
