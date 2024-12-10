using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Address;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class AddressesControllerService(
        DataContext context,
        IMapper mapper
        ) : IAddressesControllerService
    {
        public async Task CreateAsync(CreateAddressVm vm) {
            var address = mapper.Map<Address>(vm);
            
            await context.Addresses.AddAsync(address);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateAddressVm vm) { 
            var address = await context.Addresses.FirstAsync(a => a.Id == vm.Id);

            address.Street = vm.Street;
            address.HouseNumber = vm.HouseNumber;
            address.Longitude = vm.Longitude;
            address.Latitude = vm.Latitude;
            address.CityId = vm.CityId;

            await context.SaveChangesAsync();
        }

        public async Task DeleteIfExistsAsync(long id) { 
            var address = await context.Addresses.FirstOrDefaultAsync(a => a.Id == id);

            if (address is null)
                return;

            context.Remove(address);
            await context.SaveChangesAsync();
        }
    }
}
