using OnlineStore.MVC.Models;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response> Register(RegisterViewModel registerViewModel);

        Task<Response<IdentityResponse>> Login(LoginViewModel loginViewModel);

        Task<Response<IdentityResponse>> Refresh(RefreshRequest refreshRequest);

        Task<Response> Logout();

        Task<Response> ConfirmEmail(Guid userId, string token);

        Task<Response> UpdateUser(UpdateUserViewModel model);

        Task<Response> ChangeEmail(ChangeEmailViewModel model);

        Task<Response> ConfirmEmailChanging(Guid userId, string newEmail, string token);

        Task<Response> ChangePassword(ChangePasswordViewModel model);

        Task<Response> ResetPasswordRequest(ResetPasswordRequestViewModel model);

        Task<Response> ResetPassword(ResetPasswordViewModel model);
    }
}
