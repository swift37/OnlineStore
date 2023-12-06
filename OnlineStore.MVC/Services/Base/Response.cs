namespace OnlineStore.MVC.Services.Base
{
    public class Response<T>
    {
        public Response() { }

        public Response(Response response)
        {
            ValidationErrors = response.ValidationErrors;
            Success = response.Success;
        }

        public int Status { get; set; }

        public string ValidationErrors { get; set; } = string.Empty;

        public bool Success { get; set; }

        public T Data { get; set; } = default!;
    }

    public class Response
    {
        public int Status { get; set; }

        public string ValidationErrors { get; set; } = string.Empty;

        public bool Success { get; set; }
    }
}
