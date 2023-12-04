using OnlineStore.MVC.Models;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(RegisterViewModel registerViewModel);

        Task<IdentityResponse> Login(LoginViewModel loginViewModel);

        Task<IdentityResponse> Refresh(string refreshToken);

        Task Logout();
    }
}
