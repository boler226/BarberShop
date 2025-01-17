﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Barbershop;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BarbershopsController(
        DataContext context,
        IMapper mapper,
        IBarbershopControllerService service,
        IValidator<CreateBarbershopVm> createValidator,
        IValidator<UpdateBarbershopVm> updateValidator
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var barbershops = await context.Barbershops
                    .ProjectTo<BarbershopVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(barbershops);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) {
            var berbershop = await context.Barbershops
                    .ProjectTo<BarbershopVm>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(b => b.Id == id);

            return Ok(berbershop);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBarbershopVm vm) { 
            var validatorResult = await createValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.CreateAsync(vm);

            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateBarbershopVm vm) { 
            var validatorResult = await updateValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.UpdateAsync(vm);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id) { 
            await service.DeleteIfExistsAsync(id);

            return Ok();
        }
    }
}
