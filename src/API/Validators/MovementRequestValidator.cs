using Application.Models;
using FluentValidation;

namespace API.Validators
{
    public class MovementRequestValidator : AbstractValidator<MovementRequest>
    {
        public MovementRequestValidator()
        {
            //RuleFor(x => x.id)
            //    .NotEmpty().WithMessage("Id is required.")
            //    .GreaterThanOrEqualTo(0).WithMessage("Id must be greater or equal to 0.");
            RuleFor(x => x.year)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(2025, 2100).WithMessage("Year must be between 2025 and 2100.");
            RuleFor(x => x.month)
                .NotEmpty().WithMessage("Month is required.")
                .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
            RuleFor(x => x.amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");
            RuleFor(x => x.date)
                .NotEmpty().WithMessage("Date is required.");
            RuleFor(x => x.description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.category)
                .NotEmpty().WithMessage("Category is required.");
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("User Id not valid.");
        }
    }
}
