using BarberShop.ViewModels.Comment;
using BarberShop.ViewModels.Reservation;

namespace BarberShop.Services.ControllerServices.Interfaces {
    public interface ICommentsControllerService {
        Task CreateAsync(CreateCommentVm vm);
        Task UpdateAsync(UpdateCommentVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
