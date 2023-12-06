using AutoMapper;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.MVC.Services
{
    public class AuthService : HttpClientServiceBase, IAuthService
    {
        public AuthService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response> Register(RegisterViewModel registerViewModel)
        {
            var registerRequest = _mapper.Map<RegisterRequest>(registerViewModel);

            try
            {
                await _client.RegisterAsync(_usingVersion, registerRequest);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response<Models.IdentityResponse>> Login(LoginViewModel loginViewModel)
        {
            var loginRequest = _mapper.Map<LoginRequest>(loginViewModel);

            try
            {
                var response = await _client.LoginAsync(_usingVersion, loginRequest);
                return new Response<Models.IdentityResponse> 
                { 
                    Success = true, 
                    Data = new Models.IdentityResponse() 
                    { 
                        AccessToken = response.AccessToken, 
                        RefreshToken = response.RefreshToken 
                    } 
                };
            }
            catch (ApiException e)
            {
                return GenerateResponse<Models.IdentityResponse>(e);
            }
        }

        public async Task<Response<Models.IdentityResponse>> Refresh(string refreshToken)
        {
            var refreshRequest = new RefreshRequest
            {
                RefreshToken = refreshToken
            };

            try
            {
                var response = await _client.RefreshAsync(_usingVersion, refreshRequest);
                return new Response<Models.IdentityResponse>
                {
                    Success = true,
                    Data = new Models.IdentityResponse()
                    {
                        AccessToken = response.AccessToken,
                        RefreshToken = response.RefreshToken
                    }
                };
            }
            catch (ApiException e)
            {
                return GenerateResponse<Models.IdentityResponse>(e);
            }
        }

        public async Task Logout()
        {
            try
            {
                await _client.LogoutAsync(_usingVersion);
            }
            catch (Exception) { }
        }
    }
}
