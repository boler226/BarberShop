namespace BarberShop.ViewModels.Reservation
{
    public class UpdateReservationVm
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<long> ServicesId { get; set; } = new List<long>();
        public long UserId { get; set; }
    }
}
