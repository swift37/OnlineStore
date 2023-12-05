﻿namespace OnlineStore.MVC.Services.Base
{
    public class Response<T>
    {
        public Response() { }

        public Response(Response response)
        {
            Message = response.Message;
            ValidationErrors = response.ValidationErrors;
            Success = response.Success;
        }

        public string Message { get; set; } = string.Empty;

        public string ValidationErrors { get; set; } = string.Empty;

        public bool Success { get; set; }

        public T Data { get; set; } = default!;
    }

    public class Response
    {
        public string Message { get; set; } = string.Empty;

        public string ValidationErrors { get; set; } = string.Empty;

        public bool Success { get; set; }
    }
}
