using BarberShop.ViewModels.Reservation;

namespace BarberShop.ViewModels.Service
{
    public class ServiceVm
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public double Time { get; set; }
        public ReservationVm Reservation { get; set; } = null!;
    }
}
