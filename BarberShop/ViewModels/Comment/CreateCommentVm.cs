namespace BarberShop.ViewModels.Comment
{
    public class CreateCommentVm
    {
        public int Rating { get; set; }
        public string Description { get; set; } = null!;
        public long EmployeeId { get; set; }
        public long UserId { get; set; }
    }
}
