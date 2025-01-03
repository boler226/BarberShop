using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Employee;
using FluentValidation;

namespace BarberShop.Validators.Employee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeVm> {
        public CreateEmployeeValidator(IExistingEntityCheckerService checkerService, IImageValidator imageValidator) {
            RuleFor(e => e.PositionId)
                .MustAsync(checkerService.IsCorrectPositionId)
                    .WithMessage("Postition with this id is not exists");

            RuleFor(e => e.Name)
              .NotEmpty()
                  .WithMessage("Name is empty or null")
              .MaximumLength(255)
                  .WithMessage("Name is too long");

            RuleFor(e => e.Image)
                .NotNull()
                    .WithMessage("Image is not selected")
                .MustAsync(imageValidator.IsValidImageAsync)
                    .WithMessage("Image is not valid");
        }
    }
}
