﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Employee;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController(
         DataContext context,
         IMapper mapper,
         IEmployeesControllerService service,
         IValidator<CreateEmployeeVm> createValidator,
         IValidator<UpdateEmployeeVm> updateValidator
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            var employees = await context.Employees
                    .ProjectTo<EmployeeVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) { 
            var employee = await context.Employees
                    .ProjectTo<EmployeeVm>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(e => e.Id == id);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeVm vm) { 
            var validatorResult = await createValidator.ValidateAsync(vm);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.CreateAsync(vm);  

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateEmployeeVm vm) { 
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
