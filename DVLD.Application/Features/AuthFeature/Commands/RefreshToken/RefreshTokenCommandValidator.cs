using FluentValidation;

namespace DVLD.Application.Features.AuthFeature.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token must be provided.")
            .NotNull().WithMessage("Refresh token cannot be null.");

        }
    }
}
