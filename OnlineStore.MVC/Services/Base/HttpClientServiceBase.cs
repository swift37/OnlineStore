using AutoMapper;
using OnlineStore.MVC.Extensions;
using OnlineStore.MVC.Services.ApiClient;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OnlineStore.MVC.Services.Base
{
    public class HttpClientServiceBase
    {
        protected readonly IClient _client;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly string _usingVersion = "1.0";

        protected HttpContext HttpContext => _httpContextAccessor.HttpContext!;

        protected HttpRequest Request => HttpContext.Request!;

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
                case (int)HttpStatusCode.BadRequest:
                    return new Response 
                    { 
                        Success = false,
                        Status = exception.StatusCode,
                        ValidationErrors = 
                            JsonSerializer.Deserialize<BadResponse>(exception.Response)?.Errors
                            .ToValidationFailures() 
                            ?? Enumerable.Empty<ValidationFailure>()
                    };
                default:
                    return new Response 
                    { 
                        Success = false, 
                        Status = exception.StatusCode
                    };
            }
        }

        protected Response<T> GenerateResponse<T>(ApiException exception)
        {
            var response = GenerateResponse(exception);
            return new Response<T>(response);
        }

        private void AddTokenToHeaders(HttpClient client, HttpRequestMessage request, string url)
        {
            var token = Request.Cookies[Constants.Authorization.XAccessToken];

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
