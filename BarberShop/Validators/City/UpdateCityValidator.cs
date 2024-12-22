using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.City;
using FluentValidation;

namespace BarberShop.Validators.City
{
    public class UpdateCityValidator : AbstractValidator<UpdateCityVm> {
        public UpdateCityValidator(IExistingEntityCheckerService checkerService, IImageValidator imageValidator) {
            RuleFor(c => c.Id)
                .MustAsync(checkerService.IsCorrectCityId)
                    .WithMessage("City with this id is not exists");

            RuleFor(c => c.CountryId)
               .MustAsync(checkerService.IsCorrectCountryId)
                   .WithMessage("Country with this id is not exists");

            RuleFor(c => c.Name)
              .NotEmpty()
                  .WithMessage("Name is empty or null")
              .MaximumLength(255)
                  .WithMessage("Name is too long");

            RuleFor(c => c.Longitude)
              .InclusiveBetween(-180, 180)
                  .WithMessage("Longitude must be between -180 and 180 degrees");

            RuleFor(c => c.Latitude)
                .InclusiveBetween(-90, 90)
                    .WithMessage("Latitude must be between -90 and 90 degrees");

            RuleFor(c => c.Image)
                .NotNull()
                    .WithMessage("Image is not selected")
                .MustAsync(imageValidator.IsValidNullPossibleImageAsync)
                    .WithMessage("Image is not valid");
        }
    }
}
