using BarberShop.ViewModels.Address;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IAddressesControllerService
    {
        Task CreateAsync(CreateAddressVm vm);
        Task UpdateAsync(UpdateAddressVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
