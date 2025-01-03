namespace BarberShop.ViewModels.Service
{
    public class UpdateServiceVm
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public double Time { get; set; }
    }
}
