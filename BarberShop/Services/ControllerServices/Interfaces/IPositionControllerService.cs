using BarberShop.ViewModels.Position;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IPositionControllerService
    {
        Task CreateAsync(CreatePositionVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
