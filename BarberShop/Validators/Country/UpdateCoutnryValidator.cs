using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Country;
using FluentValidation;

namespace BarberShop.Validators.Country
{
    public class UpdateCoutnryValidator : AbstractValidator<UpdateCountryVm> {
        public UpdateCoutnryValidator(IExistingEntityCheckerService checkerService, IImageValidator imageValidator) {
            RuleFor(c => c.Id)
                .MustAsync(checkerService.IsCorrectCountryId)
                    .WithMessage("Country with this id is not exists");

            RuleFor(c => c.Name)
                .NotEmpty()
                    .WithMessage("Name is empty or null")
                .MaximumLength(255)
                    .WithMessage("Name is too long");

            RuleFor(c => c.Image)
                .NotNull()
                    .WithMessage("Image is not selected")
                .MustAsync(imageValidator.IsValidNullPossibleImageAsync)
                    .WithMessage("Image is not valid");
        }
    }
}
