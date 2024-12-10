using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountriesController(
            DataContext context,
            IMapper mapper,
            ICountriesControllerService service
            ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            var countries = await context.Countries
                    .ProjectTo<CountryVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) { 
            var countries = await context.Countries
                    .ProjectTo<CountryVm>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(c => c.Id == id);

            if (countries is null)
                return NotFound();

            return Ok(countries);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCountryVm vm) { 
            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCountryVm vm) { 
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
