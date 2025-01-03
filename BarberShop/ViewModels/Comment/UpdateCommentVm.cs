namespace BarberShop.ViewModels.Comment
{
    public class UpdateCommentVm
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; } = null!;
    }
}
