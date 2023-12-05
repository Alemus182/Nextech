using Application.Services.Stories.Querys;
using FluentValidation;

namespace Application.Services.Auth.Validators
{
    public class GetNewsValidator : AbstractValidator<GetNewstStoriesRequest>
    {
        public GetNewsValidator()
        {
            RuleFor(e => e.Page).GreaterThanOrEqualTo(0);
        }
    }
}
