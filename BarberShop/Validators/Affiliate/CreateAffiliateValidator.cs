using BarberShop.Database.Context;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Affiliate;
using FluentValidation;


namespace BarberShop.Validators.Affiliate
{
    public class CreateAffiliateValidator : AbstractValidator<CreateAffiliateVm> {
        public CreateAffiliateValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(a => a.CityId)
                    .MustAsync(checkerService.IsCorrectCityId)
                        .WithMessage("City with this id is not exists");

            RuleFor(a => a.Phone)
                    .NotEmpty()
                        .WithMessage("Phone number is empty or null")
                    .MaximumLength(255)
                        .WithMessage("Phone number is too long");
        }
    }
}
