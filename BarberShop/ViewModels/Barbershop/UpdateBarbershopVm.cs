namespace BarberShop.ViewModels.Barbershop
{
    public class UpdateBarbershopVm
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public long AddressId { get; set; }
        public long AffiliateId { get; set; }
    }
}
