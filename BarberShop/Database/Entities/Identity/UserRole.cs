using Microsoft.AspNetCore.Identity;

namespace BarberShop.Database.Entities.Identity
{
    public class UserRole : IdentityUserRole<long>
    {
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
