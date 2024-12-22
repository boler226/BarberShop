using BarberShop.Database.Context;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Address;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Validators.Address
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressVm> {
        public UpdateAddressValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(a => a.Id)
                    .MustAsync(checkerService.IsCorrectAddressId)
                        .WithMessage("Address with this id is not exists");

            RuleFor(a => a.CityId)
                    .MustAsync(checkerService.IsCorrectCityId)
                        .WithMessage("City with this id is not exists");

            RuleFor(a => a.HouseNumber)
                    .NotEmpty()
                        .WithMessage("House number is empty or null")
                    .MaximumLength(255)
                        .WithMessage("House number is too long");

            RuleFor(a => a.Street)
                    .NotEmpty()
                        .WithMessage("Street is empty or null")
                    .MaximumLength(255)
                        .WithMessage("Street is too long");

            RuleFor(a => a.Longitude)
                .InclusiveBetween(-180, 180)
                    .WithMessage("Longitude must be between -180 and 180 degrees");

            RuleFor(a => a.Latitude)
                .InclusiveBetween(-90, 90)
                    .WithMessage("Latitude must be between -90 and 90 degrees");
        }
    }
}
