using FluentValidation;

namespace DVLD.Application.Features.UserFeature.Quiers.GetUser
{
    public class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    if (!x.UserId.HasValue && string.IsNullOrEmpty(x.UserName))
                        // the error will raise
                        return false;
                    // no error
                    return true;
                })
                .WithMessage("You should provide at least one value for either UserId or UserName.");
        }
    }
}
