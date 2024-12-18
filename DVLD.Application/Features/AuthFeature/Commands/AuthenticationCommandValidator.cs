using DVLD.Domain.Entites.Auth;
using FluentValidation;

namespace DVLD.Application.Features.AuthFeature.Commands
{
    public class AuthenticationCommandValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Invalied Username or password.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Invalied Username or password.");
        }
    }
}
