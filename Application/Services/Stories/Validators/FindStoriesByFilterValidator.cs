using Application.Services.Stories.Querys;
using FluentValidation;

namespace Application.Services.Auth.Validators
{
    public class FindStoriesByFilterValidator : AbstractValidator<FindStoriesByFilterRequest>
    {
        string[] categoriesValues = { "", "topstories", "beststories", "askstories", "showstories", "jobstories" };
        public FindStoriesByFilterValidator()
        {
            RuleFor(e => e.Page).GreaterThanOrEqualTo(0);
            RuleFor(e => e.Category).
                Must( item => categoriesValues.Contains(item)).
                WithMessage("The list must contain the available values");
        }
    }
}
