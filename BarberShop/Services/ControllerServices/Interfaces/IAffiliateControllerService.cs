using BarberShop.ViewModels.Affiliate;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IAffiliateControllerService
    {
        Task CreateAsync(CreateAffiliateVm vm);
        Task UpdateAsync(UpdateAffiliateVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
