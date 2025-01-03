using BarberShop.ViewModels.Barbershop;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IBarbershopControllerService
    {
        Task CreateAsync(CreateBarbershopVm vm);
        Task UpdateAsync(UpdateBarbershopVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
