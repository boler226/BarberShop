using BarberShop.ViewModels.Account;
using BarberShop.ViewModels.Barbershop;
using BarberShop.ViewModels.Employee;
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
        public EmployeeVm Employee { get; set; } = null!;
        public BarbershopVm Barbersop { get; set; } = null!;
        public UserVm User { get; set; } = null!;
    }
}
