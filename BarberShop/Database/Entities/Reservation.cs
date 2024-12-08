using BarberShop.Database.Entities.Identity;

namespace BarberShop.Database.Entities
{
    public class Reservation
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public ICollection<Service> Services { get; set; } = null!;
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public long UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
