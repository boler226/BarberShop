using BarberShop.Database.Entities.Identity;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BarberShop.Services
{
    public class JwtTokenService(
        UserManager<User> userManager,
        IConfiguration configuration
        ) : IJwtTokenService {

        public async Task<JwtTokenResponse> RefreshToken(RefreshTokenVm vm) {
            var principal = GetTokenPrincipal(vm.Token);

            var response = new JwtTokenResponse();
            var userId = principal?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return response;

            var identityUser = await userManager.FindByIdAsync(userId);

            if (identityUser is null || identityUser.RefreshToken != vm.RefreshToken ||
                identityUser.RefreshTokenExpiry > DateTime.UtcNow)
                    return response;

            response.IsLogedIn = true;
            response.Token = await CreateTokenAsync(identityUser);
            response.RefreshToken = CreateRefreshToken();

            identityUser.RefreshToken = response.RefreshToken;
            identityUser.RefreshTokenExpiry = DateTime.UtcNow.AddDays(12);

            await userManager.UpdateAsync(identityUser);

            return response;
        }

        public async Task<string> CreateTokenAsync(User user) {
            var key = Encoding.UTF8.GetBytes(
                    configuration["Authentication:Jwt:SecretKey"]
                        ?? throw new NullReferenceException("Authentication:Jwt:SecretKey")
            );

            var signinKey = new SymmetricSecurityKey(key);

            var signinCredential = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredential,
                expires: DateTime.Now.AddHours(1),
                claims: await GetClaimsAsync(user));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string CreateRefreshToken() {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create()) {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetTokenPrincipal(string token) {
            var secretKey = configuration["Authentication:Jwt:SecretKey"]
            ?? throw new NullReferenceException("Authentication:Jwt:SecretKey");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var validation = new TokenValidationParameters {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        public async Task<User?> GetUserByTokenAsync(string token) {
            var principal = GetTokenPrincipal(token);

            var userId = principal?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            var identityUser = await userManager.FindByIdAsync(userId);

            return identityUser;
        }

        private async Task<List<Claim>> GetClaimsAsync(User user) {
            string userEmail = user.Email 
                    ?? throw new NullReferenceException($"User.Email");

            var userRoles = await userManager.GetRolesAsync(user);

            var roleClaims = userRoles
                    .Select(r => new Claim(ClaimTypes.Role, r))
                    .ToList();

            var claims = new List<Claim>() {
                new ("id", user.Id.ToString()),
                new ("email", userEmail),
                new ("firstName", user.FirstName),
                new ("lastName", user.LastName),
                new ("image", user.Image)
            };
            claims.AddRange(roleClaims);

            return claims;
        }
    }
}
