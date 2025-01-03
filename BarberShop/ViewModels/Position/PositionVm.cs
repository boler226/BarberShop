using BarberShop.ViewModels.Employee;

namespace BarberShop.ViewModels.Position
{
    public class PositionVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<EmployeeVm> Employees { get; set; } = null!;
    }
}
