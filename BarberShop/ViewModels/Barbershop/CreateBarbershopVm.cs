namespace BarberShop.ViewModels.Barbershop
{
    public class CreateBarbershopVm
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public long AddressId { get; set; }
        public long AffiliateId { get; set; }
    }
}
