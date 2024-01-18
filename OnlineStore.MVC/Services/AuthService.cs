using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;
using OnlineStore.MVC.Models;
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

        public async Task<Response> Register(RegisterViewModel model)
        {
            var requestBody = _mapper.Map<RegisterRequest>(model);

            try
            {
                await _client.RegisterAsync(_usingVersion, requestBody);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response<Models.IdentityResponse>> Login(LoginViewModel model)
        {
            var requestBody = _mapper.Map<LoginRequest>(model);

            try
            {
                var response = await _client.LoginAsync(_usingVersion, requestBody);

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
            var requestBody = new RefreshRequest
            {
                RefreshToken = refreshToken
            };

            try
            {
                var response = await _client.RefreshAsync(_usingVersion, requestBody);
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

        public async Task<Response> ConfirmEmail(Guid userId, string token)
        {
            var requestBody = new ConfirmEmailRequest
            {
                UserId = userId,
                Token = token
            };

            try
            {
                await _client.ConfirmEmailAsync(_usingVersion, requestBody);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response> UpdateUser(UpdateUserViewModel model)
        {
            var requestBody = _mapper.Map<UpdateUserRequest>(model);

            try
            {
                await _client.UpdateUserAsync(_usingVersion, requestBody);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response> ChangeEmail(ChangeEmailViewModel model)
        {
            var requestBody = _mapper.Map<ChangeEmailRequest>(model);

            try
            {
                await _client.ChangeEmailAsync(_usingVersion, requestBody);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response> ConfirmEmailChanging(Guid userId, string newEmail, string token)
        {
            var requestBody = new ConfirmEmailChangingRequest
            {
                UserId = userId,
                NewEmail = newEmail,
                Token = token
            };

            try
            {
                await _client.ConfirmEmailChangingAsync(_usingVersion, requestBody);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response> ChangePassword(ChangePasswordViewModel model)
        {
            var requestBody = _mapper.Map<ChangePasswordRequest>(model);

            try
            {
                await _client.ChangePasswordAsync(_usingVersion, requestBody);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response> ResetPasswordRequest(string usernameOrEmail)
        {
            try
            {
                await _client.ResetPasswordRequestAsync(usernameOrEmail, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response> ResetPassword(ResetPasswordViewModel model)
        {
            var requestBody = _mapper.Map<ResetPasswordRequest>(model);

            try
            {
                await _client.ResetPasswordAsync(_usingVersion, requestBody);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }
    }
}
