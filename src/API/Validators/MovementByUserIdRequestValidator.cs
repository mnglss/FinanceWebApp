using Application.Models;
using FluentValidation;

namespace API.Validators
{
    public class MovementByUserIdRequestValidator : AbstractValidator<MovementByUserIdRequest>
    {
        public MovementByUserIdRequestValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater or equal to 0.");
            RuleFor(x => x.year.Length)
                .NotEmpty().WithMessage("Year is required.")
                .GreaterThan(0).WithMessage("At least one Year is required.");
            RuleFor(x => x.month.Length)
                .NotEmpty().WithMessage("Month is required.")
                .GreaterThan(0).WithMessage("At least one Month is required.");
        }
    }
}
