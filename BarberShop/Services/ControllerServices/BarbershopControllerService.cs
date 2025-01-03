using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Barbershop;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class BarbershopControllerService(
        DataContext context,
        IMapper mapper
        ) : IBarbershopControllerService
    {
        public async Task CreateAsync(CreateBarbershopVm vm) { 
            var berbershop = mapper.Map<Barbershop>(vm);

            await context.Barbershops.AddAsync(berbershop);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateBarbershopVm vm) { 
            var barbershop = await context.Barbershops.FirstAsync(b => b.Id == vm.Id);

            barbershop.Name = vm.Name;
            barbershop.Phone = vm.Phone;
            barbershop.AddressId = vm.AddressId;
            barbershop.AffiliateId = vm.AffiliateId;

            await context.SaveChangesAsync();  
        }
        public async Task DeleteIfExistsAsync(long id) { 
            var barbershop = await context.Barbershops.FirstOrDefaultAsync(b => b.Id == id);

            if (barbershop is null)
                return;

            context.Barbershops.Remove(barbershop);
            await context.SaveChangesAsync();
        }
    }
}
