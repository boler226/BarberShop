using BarberShop.ViewModels.Barbershop;
using BarberShop.ViewModels.City;

namespace BarberShop.ViewModels.Affiliate
{
    public class AffiliateVm
    {
        public long Id { get; set; }
        public string Phone { get; set; } = null!;
        public CityVm City { get; set; } = null!;
        public ICollection<BarbershopVm> Barbershops { get; set; } = new List<BarbershopVm>();
    }
}
