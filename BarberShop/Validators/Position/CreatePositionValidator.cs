using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Position;
using FluentValidation;


namespace BarberShop.Validators.Position
{
    public class CreatePositionValidator : AbstractValidator<CreatePositionVm> {
        public CreatePositionValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(e => e.Name)
            .NotEmpty()
                .WithMessage("Name is empty or null")
            .MaximumLength(255)
                .WithMessage("Name is too long");
        }
    }
}
