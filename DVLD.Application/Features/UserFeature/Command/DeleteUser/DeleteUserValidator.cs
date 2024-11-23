using FluentValidation;

namespace DVLD.Application.Features.UserFeature.Command.DeleteUser
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {

            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserID is required.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserID must be greater than 0.");

        }
    }
}