using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces.Identity;
using OnlineStore.Application.Interfaces.Infrastructure;
using OnlineStore.Application.Models;
using OnlineStore.Application.Models.Identity;
using OnlineStore.Identity.Models;

namespace OnlineStore.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IJwtProvider _jwtProvider;
        private readonly IEmailSender _emailSender;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IJwtProvider jwtProvider,
            IOptions<JwtOptions> jwtOptions,
            IEmailSender emailSender) =>
            (_userManager, _signInManager, _jwtProvider, _jwtOptions, _emailSender) = 
            (userManager, signInManager, jwtProvider, jwtOptions.Value, emailSender);

        public async Task Register(RegisterRequest request)
        {
            var exitingUser = await _userManager.FindByNameAsync(request.Username);
            if (exitingUser is { }) 
                throw new Exception($"User {request.Username} alredy exists.");

            var newUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                var callbackUrl = string.Empty;

                var emailRequest = new EmailRequest
                {
                    ToEmail = newUser.Email,
                    Subject = "Password Confirmation",
                    Body = string.Format(
                    "Thank you for updating your email. Please confirm the email by clicking the following link:" +
                    "<br/><a href='{0}'>Confirm Email</a>",
                    callbackUrl)
                };

                await _emailSender.SendEmailAsync(emailRequest);
            }
            else
                foreach (var error in result.Errors)
                    throw new Exception($"Failed to register: [{error.Code}] {error.Description}");
        }

        public async Task<IdentityResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail) 
                ?? await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            if (user is null)
                throw new Exception($"Such a user doesn't exist");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new Exception($"Invalid password");

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryInDays);

            await _userManager.UpdateAsync(user);

            var response = new IdentityResponse
            {
                AccessToken = await _jwtProvider.GenerateAccessToken(user.Id),
                RefreshToken = refreshToken
            };

            return response;
        }

        public async Task<IdentityResponse> Refresh(RefreshRequest refreshRequest)
        {
            var user = await _userManager.FindByIdAsync(refreshRequest.UserId.ToString());

            if (user is null) 
                throw new NotFoundException("There is no user with this id.", nameof(ApplicationUser));
            if (user.RefreshToken != refreshRequest.RefreshToken)
                throw new SecurityTokenValidationException("Invalid refresh token.");
            if (user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new SecurityTokenExpiredException("Refresh token expired.");

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryInDays);

            await _userManager.UpdateAsync(user);

            var response = new IdentityResponse
            {
                AccessToken = await _jwtProvider.GenerateAccessToken(user.Id),
                RefreshToken = refreshToken
            };

            return response;
        }

        public async Task Logout(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) 
                throw new NotFoundException("There is no user with this id.", nameof(ApplicationUser));

            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;

            await _userManager.UpdateAsync(user);
        }

        public async Task ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                throw new Exception($"User is not found.");

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    throw new Exception($"Email has no confirmed: [{error.Code}] {error.Description}");
        }

        public async Task UpdateUser(UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                throw new Exception($"User is not found.");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.Username;
            user.DateOfBirth = request.DateOfBirth;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    throw new Exception($"User updating failed: [{error.Code}] {error.Description}");
        }

        public async Task ChangeEmail(ChangeEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                throw new Exception($"User is not found.");
            if (string.IsNullOrEmpty(user.Email))
                throw new Exception($"User email is undefined.");

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);

            var callbackUrl = string.Empty;

            var emailRequest = new EmailRequest
            {
                ToEmail = user.Email,
                Subject = "Password Reset",
                Body = string.Format(
                    "To change your email, please click on the following link:" +
                    "<br/><a href='{0}'>Change Email</a><br/>" +
                    "If you have not requested a password reset, please contact support.",
                    callbackUrl)
            };

            await _emailSender.SendEmailAsync(emailRequest);
        }

        public async Task ConfirmEmailChanging(ConfirmEmailChangingRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                throw new Exception($"User is not found.");

            var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, request.Token);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = string.Empty;

                if (string.IsNullOrEmpty(user.Email))
                    throw new Exception($"User email is undefined.");

                var emailRequest = new EmailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Password Confirmation",
                    Body = string.Format(
                        "Thank you for updating your email. Please confirm the email by clicking the following link:" +
                        "<br/><a href='{0}'>Confirm Email</a>",
                        callbackUrl)
                };

                await _emailSender.SendEmailAsync(emailRequest);
            }
            else
                foreach (var error in result.Errors)
                    throw new Exception($"Email has no changed: [{error.Code}] {error.Description}");
        }

        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                throw new Exception($"User is not found.");
            
            var result = await _userManager
                .ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    throw new Exception($"Password has no changed: [{error.Code}] {error.Description}");
        }

        public async Task ResetPasswordRequest(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new NotFoundException($"User {email} not found.", nameof(ApplicationUser));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = string.Empty;

            var emailRequest = new EmailRequest
            {
                ToEmail = user.Email,
                Subject = "Password Reset",
                Body = string.Format(
                    "To reset your password, please click on the following link:" +
                    "<br/><a href='{0}'>Reset Password</a><br/>" +
                    "If you have not requested a password reset, please contact support.",
                    callbackUrl)
            };

            await _emailSender.SendEmailAsync(emailRequest);
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                throw new Exception($"User is not found.");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    throw new Exception($"Password has no changed: [{error.Code}] {error.Description}");
        }
    }
}
