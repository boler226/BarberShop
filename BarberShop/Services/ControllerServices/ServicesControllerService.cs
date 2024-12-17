using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Service;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class ServicesControllerService(
        DataContext context,
        IMapper mapper
        ) : IServicesControllerService
    {
        public async Task CreateAsync(CreateServiceVm vm) { 
            var service = mapper.Map<Service>(vm);

            await context.Services.AddAsync(service);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateServiceVm vm) { 
            var service = await context.Services.FirstAsync(s => s.Id == vm.Id);

            service.Name = vm.Name;
            service.Price = vm.Price;
            service.Time = vm.Time;

            await context.SaveChangesAsync();
        }
        public async Task DeleteIfExistsAsync(long id) { 
            var service = await context.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (service is null)
                return;

            context.Services.Remove(service);
            await context.SaveChangesAsync();
        }
    }
}
