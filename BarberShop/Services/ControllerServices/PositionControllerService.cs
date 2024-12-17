using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Position;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class PositionControllerService(
         DataContext context,
        IMapper mapper
        ) : IPositionControllerService
    {
        public async Task CreateAsync(CreatePositionVm vm) {
            var position = mapper.Map<Position>(vm);
            
            await context.Positions.AddAsync(position);
            await context.SaveChangesAsync();
        }

        public async Task DeleteIfExistsAsync(long id) { 
            var position = await context.Positions.FirstOrDefaultAsync(p => p.Id == id);

            if (position is null)
                return;

            context.Positions.Remove(position);
            await context.SaveChangesAsync();
        }
    }
}
