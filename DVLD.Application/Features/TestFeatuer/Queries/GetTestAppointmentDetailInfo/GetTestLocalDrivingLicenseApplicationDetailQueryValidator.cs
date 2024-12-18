using FluentValidation;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestAppointmentDetailInfo
{

    public class GetTestLocalDrivingLicenseApplicationDetailQueryValidator

        : AbstractValidator<GetTestLocalDrivingLicenseApplicationDetailQuery>
    {
        public GetTestLocalDrivingLicenseApplicationDetailQueryValidator()
        {
            RuleFor(query => query.LocalDrivingLicenseApplicationId)
                .GreaterThan(0).WithMessage("The LocalDrivingLicenseApplicationId must be greater than zero.")
                .NotEmpty().WithMessage("The LocalDrivingLicenseApplicationId must not be empty.");
        }
    }
}
