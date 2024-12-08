using Microsoft.AspNetCore.Identity;

namespace BarberShop.Database.Entities.Identity
{
    public class Role : IdentityRole<long>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
