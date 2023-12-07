using OnlineStore.MVC.Models;
using OnlineStore.MVC.Services.Base;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response> Register(RegisterViewModel registerViewModel);

        Task<Response<IdentityResponse>> Login(LoginViewModel loginViewModel);

        Task<Response<IdentityResponse>> Refresh(string refreshToken);

        Task<Response> Logout();
    }
}
