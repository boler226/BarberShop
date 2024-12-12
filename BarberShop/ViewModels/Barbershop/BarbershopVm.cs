using BarberShop.ViewModels.Address;
using BarberShop.ViewModels.Affiliate;

namespace BarberShop.ViewModels.Barbershop
{
    public class BarbershopVm
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public AddressVm Address { get; set; } = null!;
        public AffiliateVm Affiliate { get; set; } = null!;
    }
}
