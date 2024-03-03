namespace StarFood.Application.Base.Messages
{
    public interface ICommandResponse
    {
        int Id { get; set; }
        object? Object { get; set; }
        bool IsValid { get; }
        Exception? Exception { get; set; }
        string? Message { get; set; }
    }
}
