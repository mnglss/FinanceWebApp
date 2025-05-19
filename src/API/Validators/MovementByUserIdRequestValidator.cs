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
            RuleFor(x => x.years)
                .NotEmpty().WithMessage("Year is required.")
                .Custom((years, context) =>
                {
                    if ((years.Length <= 0) || (string.IsNullOrWhiteSpace(years)))
                        context.AddFailure("The years list must contain at least one element.");
                });
            RuleFor(x => x.months)
                .NotEmpty().WithMessage("Month is required.")
                .Custom((months, context) =>
                {
                    if (months.Length <= 0)
                        context.AddFailure("The months list must contain at least one element.");
                    if (listIsNotValid(months))
                        context.AddFailure("The months list is not valid!.");
                });
        }

        private bool listIsNotValid(string months)
        {
            var monthList = months.Split(',').Select(int.Parse).Distinct().ToList();
            if (monthList.Count == 0) return true;
            if (monthList.Min() < 1 || monthList.Max() > 12) return true;
            return false;
        }
    }
}
