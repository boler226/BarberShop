using BarberShop.ViewModels.Service;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IServicesControllerService
    {
        Task CreateAsync(CreateServiceVm vm);
        Task UpdateAsync(UpdateServiceVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
