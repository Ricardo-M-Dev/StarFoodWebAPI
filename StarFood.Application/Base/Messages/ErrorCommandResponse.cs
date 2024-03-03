namespace StarFood.Application.Base.Messages
{
    public class ErrorCommandResponse : ICommandResponse
    {

        public ErrorCommandResponse()
        {
            IsValid = false;
            Message = "Não encontrado.";
        }

        public ErrorCommandResponse(Exception? exception)
        {
            IsValid = false;
            Exception = exception;
        }

        public int Id { get; set; }
        public bool IsValid { get; }
        public Exception? Exception { get; set; }
        public object? Object { get; set; }
        public string? Message { get; set; }
    }
}
