using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;
using OnlineStore.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineStore.MVC.Services
{
    public class AuthService : HttpClientServiceBase, IAuthService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) =>
            _tokenHandler = new JwtSecurityTokenHandler();

        public async Task<Response> Register(RegisterViewModel registerViewModel)
        {
            var registerRequest = _mapper.Map<RegisterRequest>(registerViewModel);

            try
            {
                await _client.RegisterAsync(_usingVersion, registerRequest);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response<Models.IdentityResponse>> Login(LoginViewModel loginViewModel)
        {
            var loginRequest = _mapper.Map<LoginRequest>(loginViewModel);

            try
            {
                var response = await _client.LoginAsync(_usingVersion, loginRequest);

                if (string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken)) 
                    return new Response<Models.IdentityResponse> { Success = false };

                var tokenContent = _tokenHandler.ReadJwtToken(response.AccessToken);
                var claims = tokenContent.Claims;
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

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
            catch (ApiException exception)
            {
                return GenerateResponse<Models.IdentityResponse>(exception);
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
            catch (ApiException exception)
            {
                return GenerateResponse<Models.IdentityResponse>(exception);
            }
        }

        public async Task<Response> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await _client.LogoutAsync(_usingVersion);

                return new Response { Success = true };
            }
            catch (ApiException exception) 
            {
                return GenerateResponse(exception);
            }
        }
    }
}
