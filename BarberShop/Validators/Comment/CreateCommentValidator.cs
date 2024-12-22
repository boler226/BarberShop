using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Comment;
using FluentValidation;

namespace BarberShop.Validators.Comment
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentVm> {
        public CreateCommentValidator(IExistingEntityCheckerService checkerService) {
            RuleFor(c => c.EmployeeId)
                .MustAsync(checkerService.IsCorrectEmployeeId)
                    .WithMessage("Employee with this id is not exists");

            RuleFor(c => c.UserId)
                .MustAsync(checkerService.IsCorrectUserId)
                    .WithMessage("User with this id is not exists");

            RuleFor(c => c.Rating)
                .InclusiveBetween(0, 6)
                    .WithMessage("Rating must be between 0 and 6.");

            RuleFor(c => c.Description)
                .NotEmpty()
                    .WithMessage("Description is empty or null")
                .MaximumLength(1000)
                    .WithMessage("Description is too long (1000)");
        }
    }
}
