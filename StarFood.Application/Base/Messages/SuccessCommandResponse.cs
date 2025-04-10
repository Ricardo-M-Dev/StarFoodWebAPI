﻿namespace StarFood.Application.Base.Messages
{
    public class SuccessCommandResponse : ICommandResponse
    {
        public SuccessCommandResponse()
        {
            IsValid = true;
        }

        public SuccessCommandResponse(object obj)
        {
            IsValid = true;
            Object = obj;
        }

        public SuccessCommandResponse(int id, object obj)
        {
            IsValid = true;
            Object = obj;
            Id = id;
        }

        public int Id { get; set; }
        public object? Object { get; set; }
        public bool IsValid { get; }
        public Exception? Exception { get; set; }
        public string? Message { get; set; }

    }
}
