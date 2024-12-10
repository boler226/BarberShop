using BarberShop.ViewModels.City;

namespace BarberShop.ViewModels.Country
{
    public class CreateCountryVm
    {
        public string Name { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}
