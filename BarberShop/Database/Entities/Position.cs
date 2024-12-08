namespace BarberShop.Database.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
