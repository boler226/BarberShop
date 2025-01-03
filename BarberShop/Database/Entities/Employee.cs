namespace BarberShop.Database.Entities
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int Rating { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
        public long BarbershopId { get; set; }
        public Barbershop Barbershop { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
