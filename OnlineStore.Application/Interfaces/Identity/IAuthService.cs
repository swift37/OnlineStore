using OnlineStore.Application.Models.Identity;

namespace OnlineStore.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<IdentityResponse> Login(LoginRequest request);
        
        Task Register(RegisterRequest request);

        Task<IdentityResponse> Refresh(RefreshRequest refreshRequest, string userId);

        Task Logout(string userId);
    }
}
