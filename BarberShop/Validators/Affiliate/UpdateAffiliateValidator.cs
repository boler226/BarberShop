using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Affiliate;
using FluentValidation;

namespace BarberShop.Validators.Affiliate
{
    public class UpdateAffiliateValidator : AbstractValidator<UpdateAffiliateVm> {
        public UpdateAffiliateValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(a => a.Id)
                    .MustAsync(checkerService.IsCorrectAffiliateId)
                         .WithMessage("Affiliate with this id is not exists");

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
