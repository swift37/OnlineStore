using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces.Identity;
using OnlineStore.Application.Models.Identity;
using OnlineStore.Identity.Models;

namespace OnlineStore.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtProvider jwtProvider) =>
            (_userManager, _signInManager, _jwtProvider) = (userManager, signInManager, jwtProvider);

        public async Task Register(RegisterRequest request)
        {
            var exitingUser = await _userManager.FindByEmailAsync(request.Email);
            if (exitingUser is { }) 
                throw new Exception($"User with mail {request.Email} alredy exists.");

            var newUser = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(newUser, "User");
            else
                foreach (var error in result.Errors)
                    throw new Exception($"Failed to register: [{error.Code}] {error.Description}");
        }

        public async Task<IdentityResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new NotFoundException($"User with mail {request.Email} not found.", nameof(ApplicationUser));

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new NotFoundException($"Credentials for {request.Email} are not valid.", nameof(ApplicationUser));

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            var response = new IdentityResponse
            {
                AccessToken = await _jwtProvider.GenerateAccessToken(user.Id),
                RefreshToken = refreshToken
            };

            return response;
        }

        public async Task<IdentityResponse> Refresh(RefreshRequest refreshRequest, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) 
                throw new NotFoundException("There is no user with this id.", nameof(ApplicationUser));
            if (user.RefreshToken != refreshRequest.RefreshToken)
                throw new SecurityTokenValidationException("Invalid refresh token.");
            if (user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new SecurityTokenExpiredException("Refresh token expired.");

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

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
    }
}
