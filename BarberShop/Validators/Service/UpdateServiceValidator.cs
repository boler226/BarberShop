using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Service;
using FluentValidation;

namespace BarberShop.Validators.Service
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceVm> {
        public UpdateServiceValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(s => s.Id)
                .MustAsync(checkerService.IsCorrectServiceId)
                    .WithMessage("Service with this id is not exists");

            RuleFor(s => s.Name)
              .NotEmpty()
                  .WithMessage("Name is empty or null")
              .MaximumLength(255)
                  .WithMessage("Name is too long");

            RuleFor(s => s.Price)
                .NotNull()
                    .WithMessage("Price is empty or null");

            RuleFor(s => s.Time)
               .NotNull()
                   .WithMessage("Time is empty or null");
        }
    }
}
