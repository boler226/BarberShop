using BarberShop.ViewModels.City;

namespace BarberShop.ViewModels.Address
{
    public class AddressVm
    {
        public long Id { get; set; }
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public CityVm City { get; set; } = null!;
    }
}
