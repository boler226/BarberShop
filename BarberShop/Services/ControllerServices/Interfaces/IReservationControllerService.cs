using BarberShop.ViewModels.Reservation;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IReservationControllerService
    {
        Task CreateAsync(CreateReservationVm vm);
        Task UpdateAsync(UpdateReservationVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
