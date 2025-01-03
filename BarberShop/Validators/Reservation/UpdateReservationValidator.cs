using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Reservation;
using FluentValidation;

namespace BarberShop.Validators.Reservation
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationVm> {
        public UpdateReservationValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(r => r.Id)
                .MustAsync(checkerService.IsCorrectReservationId)
                    .WithMessage("Reservation with this id is not exists");

            RuleFor(r => r.ServicesId)
              .MustAsync(checkerService.IsCorrectServicesId)
                  .WithMessage("One of services does not exist");

            RuleFor(r => r.UserId)
                .MustAsync(checkerService.IsCorrectUserId)
                    .WithMessage("User with this id is not exists");

            RuleFor(r => r.From)
                .GreaterThan(DateTime.Now)
                    .WithMessage("Reservations are only possible for the future");

            RuleFor(r => r.From)
                .LessThan(r => r.To)
                    .WithMessage("Invalid time span");

            RuleFor(r => r.Price)
                .NotNull()
                    .WithMessage("Price is empty or null");
        }
    }
}
