using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Address;
using BarberShop.ViewModels.City;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CitiesController(
        DataContext context,
        IMapper mapper,
        ICitiesControllerService service,
        IValidator<CreateCityVm> createValidator,
        IValidator<UpdateCityVm> updateValidator
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var cities = await context.Cities
                    .ProjectTo<CityVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) {
            var city = await context.Cities
                    .ProjectTo<CityVm>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(c => c.Id == id);

            if (city is null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCityVm vm) { 
            var validatorResult = await createValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCityVm vm) { 
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
