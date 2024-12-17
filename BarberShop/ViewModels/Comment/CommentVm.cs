using BarberShop.ViewModels.Account;
using BarberShop.ViewModels.Employee;

namespace BarberShop.ViewModels.Comment
{
    public class CommentVm
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; } = null!;
        public EmployeeVm Employee { get; set; } = null!;
        public UserVm User { get; set; } = null!;
    }
}
