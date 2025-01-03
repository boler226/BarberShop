using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Address;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressesController(
        DataContext context,
        IMapper mapper,
        IAddressesControllerService service,
        IValidator<CreateAddressVm> createValidator,
        IValidator<UpdateAddressVm> updateValidator

        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            var addresses = await context.Addresses
                    .ProjectTo<AddressVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) { 
            var address = await context.Addresses
                    .ProjectTo<AddressVm>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (address is null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAddressVm vm) {
            var validatorResult = await createValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid) 
                return BadRequest(validatorResult.Errors);
            
            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateAddressVm vm) { 
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
