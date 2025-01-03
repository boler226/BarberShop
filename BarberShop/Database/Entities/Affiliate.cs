namespace BarberShop.Database.Entities
{
    public class Affiliate
    {
        public long Id { get; set; }
        public string Phone { get; set; } = null!;
        public long CityId { get; set; }
        public City City { get; set; } = null!;
        public ICollection<Barbershop> Barbershops { get; set; } = new List<Barbershop>();
    }
}
