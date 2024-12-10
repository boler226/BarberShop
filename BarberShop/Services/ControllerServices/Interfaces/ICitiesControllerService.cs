using BarberShop.ViewModels.City;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface ICitiesControllerService
    {
        Task CreateAsync(CreateCityVm vm);
        Task UpdateAsync(UpdateCityVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
