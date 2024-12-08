using Microsoft.AspNetCore.Identity;

namespace BarberShop.Database.Entities.Identity
{
    public class User : IdentityUser<long> 
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Image {  get; set; } = null!;
        public virtual ICollection<UserRole> UserRoles { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
