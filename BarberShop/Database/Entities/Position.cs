namespace BarberShop.Database.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
