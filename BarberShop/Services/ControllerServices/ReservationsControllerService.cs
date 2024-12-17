using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Reservation;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class ReservationsControllerService(
        DataContext context,
        IMapper mapper
        ) : IReservationControllerService
    {
        public async Task CreateAsync(CreateReservationVm vm) {
            var reservation = mapper.Map<Reservation>(vm);

            if (vm.ServicesId != null && vm.ServicesId.Any())
            {
                foreach (var serviceId in vm.ServicesId)
                {
                    reservation.ReservationService.Add(new ReservationService
                    {
                        ServiceId = serviceId
                    });
                }
            }

            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateReservationVm vm) { 
            var reservation = await context.Reservations.FirstAsync(r => r.Id == vm.Id);

            if (reservation is null)
                return;

            reservation.Price = vm.Price;
            reservation.From = vm.From;
            reservation.To = vm.To;
            reservation.UserId = vm.UserId;

            if (vm.ServicesId != null && vm.ServicesId.Any())
            {
                reservation.ReservationService.Clear();
                foreach (var serviceId in vm.ServicesId)
                {
                    reservation.ReservationService.Add(new ReservationService
                    {
                        ServiceId = serviceId
                    });
                }
            }

            await context.SaveChangesAsync();
        }
        public async Task DeleteIfExistsAsync(long id) { 
            var reservation = await context.Reservations.FirstOrDefaultAsync(r => r.Id == id); 

            if (reservation is null)
                return;

            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();
        }
    }
}
