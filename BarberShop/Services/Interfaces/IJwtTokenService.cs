using BarberShop.Database.Entities.Identity;
using System.Security.Claims;

namespace BarberShop.Services.Interfaces
{
    public interface IJwtTokenService {
        Task<string> CreateTokenAsync(User user);
    }
}
