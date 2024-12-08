namespace BarberShop.Database.Entities
{
    public class Service
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public double Time { get; set; }
        public long ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
    }
}
