namespace StarFood.Application.Base.Messages
{
    public class ErrorCommandResponse : ICommandResponse
    {

        public ErrorCommandResponse()
        {
            IsValid = false;
        }

        public ErrorCommandResponse(Exception? exception)
        {
            IsValid = false;
            Exception = exception;
        }
        public bool IsValid { get; }
        public Exception? Exception { get; set; }
        public object? Object { get; set; }
    }
}
