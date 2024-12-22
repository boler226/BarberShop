using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Affiliate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AffiliatesController(
         DataContext context,
        IMapper mapper,
        IAffiliateControllerService service,
        IValidator<CreateAffiliateVm> createValidator,
        IValidator<UpdateAffiliateVm> updateValidator
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var affiliate = await context.Affiliates
                    .ProjectTo<AffiliateVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(affiliate);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) {
            var affiliate = await context.Affiliates
                     .ProjectTo<AffiliateVm>(mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(a => a.Id == id);

            if (affiliate is null)
                return NotFound();

            return Ok(affiliate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAffiliateVm vm) { 
            var validatorResult = await createValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.CreateAsync(vm);

            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateAffiliateVm vm) { 
            var validatorResult = await updateValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.UpdateAsync(vm);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) { 
            await service.DeleteIfExistsAsync(id);

            return Ok();
        }
    }
}
