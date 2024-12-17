using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Position;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PositionsController(
        DataContext context,
        IMapper mapper,
        IPositionControllerService service
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            var positions = await context.Positions
                    .ProjectTo<PositionVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) { 
            var position = await context.Positions
                    .ProjectTo<PositionVm>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.Id == id);

            if (position is null)
                return NotFound();

            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVm vm) { 
            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) { 
            await service.DeleteIfExistsAsync(id);

            return Ok();
        }
    }
}
