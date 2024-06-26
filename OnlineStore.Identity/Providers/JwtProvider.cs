﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces.Identity;
using OnlineStore.Domain.Constants;
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

        public JwtProvider(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> options) =>
            (_userManager, _jwtOptions) = (userManager, options.Value);

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
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Birthdate, 
                    user.DateOfBirth.HasValue ? 
                    user.DateOfBirth.Value.ToString("yyyy-MM-dd") :
                    string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaimNames.RegistrationDate, user.DateOfRegistration.ToString("yyyy-MM-dd")),
                new Claim(CustomClaimNames.Phone, user.PhoneNumber ?? string.Empty)
            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiryInMinutes),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken() =>
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
    }
}
