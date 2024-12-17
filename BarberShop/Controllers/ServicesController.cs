using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicesController(
        DataContext context,
        IMapper mapper,
        IServicesControllerService service
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var services = await context.Services
                    .ProjectTo<ServiceVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) { 
            var services = await context.Services
                    .ProjectTo<ServiceVm>(mapper.ConfigurationProvider)
                    .FirstAsync(s => s.Id == id);

            if (services is null) 
                return NotFound();

            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateServiceVm vm) { 
            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateServiceVm vm) { 
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
