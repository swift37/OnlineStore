using AutoMapper;
using OnlineStore.MVC.Services.ApiClient;
using System.Net;

namespace OnlineStore.MVC.Services.Base
{
    public class HttpClientServiceBase
    {
        protected readonly IClient _client;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly string _usingVersion = "1.0";

        protected HttpRequest Request => _httpContextAccessor.HttpContext?.Request!;

        public HttpClientServiceBase(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

            _client.RequestPreparation = AddTokenToHeaders;
        }

        protected Response GenerateResponse(ApiException exception)
        {
            switch (exception.StatusCode)
            {
                case (int)HttpStatusCode.NotFound:
                    return new Response
                    {
                        Success = false,
                        Message = "The requested item is not found."
                    };
                case (int)HttpStatusCode.Unauthorized:
                    return new Response
                    {
                        Success = false,
                        Message = "User is unauthorized."
                    };
                case (int)HttpStatusCode.Forbidden:
                    return new Response
                    {
                        Success = false,
                        Message = "It is not enough access to perform this action."
                    };
                case (int)HttpStatusCode.UnprocessableEntity:
                    return new Response
                    {
                        Success = false,
                        Message = "It is unpossible to process this entity."
                    };
                case (int)HttpStatusCode.BadRequest:
                    return new Response 
                    { 
                        Success = false, 
                        Message = "Validation error has occured.",
                        ValidationErrors = exception.Response
                    };
                default:
                    return new Response { Success = false, Message = exception.Message };
            }
        }

        private void AddTokenToHeaders()
        {
            var token = Request.Cookies[Constants.Authorization.XAccessToken]; 

            if (!string.IsNullOrWhiteSpace(token))
            {
                if (Request.Headers.ContainsKey(Constants.Authorization.Key)) 
                    Request.Headers.Remove(Constants.Authorization.Key);

                Request.Headers.TryAdd(Constants.Authorization.Key, $"Bearer {token}");
            }
            else
                Request.Headers?
                    .TryAdd(Constants.Authorization.Key, string.Empty);
        }
    }
}
