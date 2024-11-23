using DVLD.Application.DTO.Users;
using FluentValidation;

namespace DVLD.Application.Features.UserFeature.Command.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {

            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserID is required.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserID must be greater than 0.");


            RuleFor(x => x.UserName)
                .NotNull().WithMessage("UserName is required.");

            RuleFor(x => x.UserName)
                .MaximumLength(20).WithMessage("UserName cannot exceed 20 characters.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.");

            RuleFor(x => x.Password)
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive is required.");

        }
    }
}