using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Employee;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class EmployeesControllerService(
         DataContext context,
        IMapper mapper,
        IImageService imageService
        ) : IEmployeesControllerService
    {
        public async Task CreateAsync(CreateEmployeeVm vm) {
            var employee = mapper.Map<Employee>(vm);
            employee.Image = await imageService.SaveImageAsync(vm.Image);

            await context.Employees.AddAsync(employee);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception) 
            {
                imageService.DeleteImageIfExists(employee.Image);
                throw;
            }
        }
        public async Task UpdateAsync(UpdateEmployeeVm vm) { 
            var employee = await context.Employees.FirstAsync(e => e.Id == vm.Id);  

            string oldImage = employee.Image;

            employee.Name = vm.Name;
            employee.Rating = vm.Rating;
            employee.PositionId = vm.PositionId;

            employee.Image = await imageService.SaveImageAsync(vm.Image);

            try
            {
                await context.SaveChangesAsync();
                imageService.DeleteImageIfExists(oldImage);
            }
            catch (Exception)
            {
                imageService.DeleteImageIfExists(employee.Image);
                throw;
            }
        }
        public async Task DeleteIfExistsAsync(long id) { 
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee is  null) 
                return;

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            imageService.DeleteImageIfExists(employee.Image);
        }
    }
}
