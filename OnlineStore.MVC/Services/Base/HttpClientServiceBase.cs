using OnlineStore.MVC.Services.ApiClient;
using System.Net;

namespace OnlineStore.MVC.Services.Base
{
    public class HttpClientServiceBase
    {
        protected readonly IClient _client;

        public HttpClientServiceBase(IClient client) => _client = client;

        protected Response<Guid> GenerateResponse(ApiException exception)
        {
            switch (exception.StatusCode)
            {
                case (int)HttpStatusCode.NotFound:
                    return new Response<Guid>
                    {
                        Success = false,
                        Message = "The requested item is not found."
                    };
                case (int)HttpStatusCode.Unauthorized:
                    return new Response<Guid>
                    {
                        Success = false,
                        Message = "User is unauthorized."
                    };
                case (int)HttpStatusCode.Forbidden:
                    return new Response<Guid>
                    {
                        Success = false,
                        Message = "It is not enough access to perform this action."
                    };
                case (int)HttpStatusCode.UnprocessableEntity:
                    return new Response<Guid>
                    {
                        Success = false,
                        Message = "It is unpossible to process this entity."
                    };
                case (int)HttpStatusCode.BadRequest:
                    return new Response<Guid> 
                    { 
                        Success = false, 
                        Message = "Validation error has occured.",
                        ValidationErrors = exception.Response
                    };
                default:
                    return new Response<Guid> { Success = false, Message = exception.Message };
            }
        }
    }
}
