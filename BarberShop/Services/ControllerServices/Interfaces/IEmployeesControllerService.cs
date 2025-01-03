using BarberShop.ViewModels.Employee;

namespace BarberShop.Services.ControllerServices.Interfaces
{
    public interface IEmployeesControllerService
    {
        Task CreateAsync(CreateEmployeeVm vm);
        Task UpdateAsync(UpdateEmployeeVm vm);
        Task DeleteIfExistsAsync(long id);
    }
}
