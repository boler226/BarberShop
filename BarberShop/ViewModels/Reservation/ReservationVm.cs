using BarberShop.ViewModels.Account;
using BarberShop.ViewModels.Service;

namespace BarberShop.ViewModels.Reservation
{
    public class ReservationVm
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public ICollection<ServiceVm> Services { get; set; } = null!;
        public UserVm User { get; set; } = null!;
    }
}
