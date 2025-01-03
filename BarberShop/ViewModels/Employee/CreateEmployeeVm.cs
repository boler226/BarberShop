namespace BarberShop.ViewModels.Employee
{
    public class CreateEmployeeVm
    {
        public string Name { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public int Rating { get; set; }
        public int PositionId { get; set; }
        public long BarbershopId { get; set; }
    }
}
