using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservationsController(
        DataContext context,
        IMapper mapper,
        IReservationControllerService service
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var reservations = await context.Reservations
                .ProjectTo<ReservationVm>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) {
            var reservation = await context.Reservations
                .ProjectTo<ReservationVm>(mapper.ConfigurationProvider)
                .FirstAsync(r => r.Id == id);

            if (reservation is null) 
                return NotFound();

            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateReservationVm vm) { 
            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateReservationVm vm) { 
            await service.UpdateAsync(vm);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) { 
            await service.DeleteIfExistsAsync(id);

            return Ok();
        }
    }
}
