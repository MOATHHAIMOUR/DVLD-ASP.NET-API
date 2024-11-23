using DVLD.Application.DTO.Users;
using FluentValidation;

namespace DVLD.Application.Features.UserFeature.Command.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserDTO>
    {
        public AddUserValidator()
        {


            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage("PersonID must be greater than 0.");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("UserName is required.");

            RuleFor(x => x.UserName)
                .MaximumLength(20).WithMessage("UserName cannot exceed 20 characters.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.")
             .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");


            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive is required.");

        }
    }
}