namespace BarberShop.ViewModels.Employee
{
    public class UpdateEmployeeVm
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public int Rating { get; set; }
        public int PositionId { get; set; }
        public long BarbershopId { get; set; }
    }
}