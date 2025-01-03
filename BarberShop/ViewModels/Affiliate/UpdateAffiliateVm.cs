namespace BarberShop.ViewModels.Affiliate
{
    public class UpdateAffiliateVm
    {
        public long Id { get; set; }
        public string Phone { get; set; } = null!;
        public long CityId { get; set; }
    }
}
