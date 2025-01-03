using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Affiliate;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class AffiliateControllerService(
        DataContext context,
        IMapper mapper
        ) : IAffiliateControllerService
    {
        public async Task CreateAsync(CreateAffiliateVm vm) { 
            var affiliate = mapper.Map<Affiliate>(vm);

            await context.Affiliates.AddAsync(affiliate);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateAffiliateVm vm) { 
            var affiliate = await context.Affiliates.FirstAsync(a => a.Id == vm.Id);

            affiliate.Phone = vm.Phone;
            affiliate.CityId = vm.CityId;

            await context.SaveChangesAsync();
        }
        public async Task DeleteIfExistsAsync(long id) { 
            var affiliate = await context.Affiliates.FirstOrDefaultAsync(a => a.Id == id);

            if (affiliate is null)
                return;

            context.Remove(affiliate);
            await context.SaveChangesAsync();
        }
    }
}
