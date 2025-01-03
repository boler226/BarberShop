using BarberShop.Database.Entities.Identity;
using BarberShop.ViewModels.Account;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IAccountsControllerService {
        Task<User> SignUpAsync(RegisterVm vm);
    }
}
