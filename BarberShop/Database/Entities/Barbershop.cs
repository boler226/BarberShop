namespace BarberShop.Database.Entities
{
    public class Barbershop
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public long AddressId { get; set; }
        public Address Adddress { get; set; } = null!;
        public long AffiliateId { get; set; }
        public Affiliate Affiliate { get; set; } = null!;
        public ICollection<Employee> Employers { get; set; } = new List<Employee>();
    }
}
