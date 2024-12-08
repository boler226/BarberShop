namespace BarberShop.Database.Entities
{
    public class CountryCities
    {
        public long CountryId { get; set; }
        public Country Country { get; set; } = null!;
        public long CityId { get; set; }
        public City City { get; set; } = null!;
    }
}
