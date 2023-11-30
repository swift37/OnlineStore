using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
        private readonly JwtOptions _jwtOptions;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IJwtProvider jwtProvider,
            IOptions<JwtOptions> jwtOptions) =>
            (_userManager, _signInManager, _jwtProvider, _jwtOptions) = 
            (userManager, signInManager, jwtProvider, jwtOptions.Value);

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
                await _userManager.AddToRoleAsync(newUser, "User");
            else
                foreach (var error in result.Errors)
                    throw new Exception($"Failed to register: [{error.Code}] {error.Description}");
        }

        public async Task<IdentityResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail) 
                ?? await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            if (user is null)
                throw new NotFoundException($"User {request.UsernameOrEmail} not found.", nameof(ApplicationUser));

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new NotFoundException($"Credentials for {request.UsernameOrEmail} are not valid.", nameof(ApplicationUser));

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
