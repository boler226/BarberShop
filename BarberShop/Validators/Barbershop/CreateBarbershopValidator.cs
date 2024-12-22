using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Barbershop;
using FluentValidation;

namespace BarberShop.Validators.Barbershop
{
    public class CreateBarbershopValidator : AbstractValidator<CreateBarbershopVm> {
        public CreateBarbershopValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(b => b.AddressId)
                .MustAsync(checkerService.IsCorrectAddressId)
                    .WithMessage("Address with this id is not exists");

            RuleFor(b => b.AffiliateId)
                .MustAsync(checkerService.IsCorrectAffiliateId)
                    .WithMessage("Affiliate with this id is not exists");

            RuleFor(b => b.Name)
                .NotEmpty()
                    .WithMessage("Name is empty or null")
                .MaximumLength(255)
                    .WithMessage("Name is too long");

            RuleFor(b => b.Phone)
                 .NotEmpty()
                     .WithMessage("Phone number is empty or null")
                 .MaximumLength(255)
                     .WithMessage("Phone number is too long");
        }
    }
}
