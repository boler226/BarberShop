using BarberShop.ViewModels.Comment;
using BarberShop.ViewModels.Position;
using BarberShop.ViewModels.Reservation;

namespace BarberShop.ViewModels.Employee
{
    public class EmployeeVm
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int Rating { get; set; }
        public long PositionId { get; set; }
        public PositionVm Position { get; set; } = null!;
        public ICollection<CommentVm> Comments { get; set; } = null!;
        public ICollection<ReservationVm> Reservations { get; set; } = null!;
    }
}
