using BarberShop.Database.Entities.Identity;

namespace BarberShop.Database.Entities
{
    public class Comment
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; } = null!;
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
