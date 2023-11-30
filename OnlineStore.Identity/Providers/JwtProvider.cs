using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces.Identity;
using OnlineStore.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineStore.Identity.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions) =>
            (_userManager, _jwtOptions) = (userManager, jwtOptions.Value);

        public async Task<string> GenerateAccessToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? 
                throw new NotFoundException("There is no user with this id.", nameof(ApplicationUser));

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiryInMinutes),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken() =>
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
    }
}
