using OnlineStore.Application.Models.Identity;

namespace OnlineStore.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<IdentityResponse> Login(LoginRequest request);
        
        Task Register(RegisterRequest request);

        Task<IdentityResponse> Refresh(RefreshRequest refreshRequest);

        Task Logout(Guid userId);

        Task ConfirmEmail(ConfirmEmailRequest request);

        Task UpdateUser(UpdateUserRequest request);

        Task ChangeEmail(ChangeEmailRequest request);

        Task ConfirmEmailChanging(ConfirmEmailChangingRequest request);

        Task ChangePassword(ChangePasswordRequest request);

        Task ResetPasswordRequest(string usernameOrEmail);

        Task ResetPassword(ResetPasswordRequest request);
    }
}
