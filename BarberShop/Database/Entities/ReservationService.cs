namespace BarberShop.Database.Entities
{
    public class ReservationService
    {
        public long ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;

        public long ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
