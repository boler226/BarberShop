using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicesController(
        DataContext context,
        IMapper mapper,
        IServicesControllerService service,
        IValidator<CreateServiceVm> createValidator,
        IValidator<UpdateServiceVm> updateValidator
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
            var validatorResult = await createValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateServiceVm vm) { 
            var validatorResult = await updateValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

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
