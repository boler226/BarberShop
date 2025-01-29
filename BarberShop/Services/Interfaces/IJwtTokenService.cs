using BarberShop.Database.Entities.Identity;
using BarberShop.ViewModels.Account;
using System.Security.Claims;

namespace BarberShop.Services.Interfaces
{
    public interface IJwtTokenService {
        Task<JwtTokenResponse> RefreshToken(RefreshTokenVm vm);
        Task<string> CreateTokenAsync(User user);
        public string CreateRefreshToken();
        Task<User> GetUserByTokenAsync(string token);
    }
}
