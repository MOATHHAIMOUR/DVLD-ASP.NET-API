namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicense
{
    using FluentValidation;

    public class LocalDrivingLicenseApplicationDtoValidator : AbstractValidator<AddNewLocalDrivingLicenseCommand>
    {
        public LocalDrivingLicenseApplicationDtoValidator()
        {
            // Validate ApplicantPersonId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.ApplicantPersonId)
                .GreaterThan(0)
                .WithMessage("ApplicantPersonId must be greater than 0.");

            // Validate ApplicationDate
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.ApplicationDate)
                .NotEmpty()
                .WithMessage("ApplicationDate is required.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("ApplicationDate cannot be in the future.");

            // Validate ApplicationTypeId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.ApplicationTypeId)
                .IsInEnum()
                .WithMessage("ApplicationTypeId must be a valid value from EnumApplicationType.");

            // Validate ApplicationStatus
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.ApplicationStatus)
                .IsInEnum()
                .WithMessage("ApplicationStatus must be a valid value from EnumApplicationStatus.");

            // Validate LicenseClassId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.LicenseClassId)
                .IsInEnum()
                .WithMessage("LicenseClassId must be a valid value from EnumLicenseClass.");

            // Validate LastStatusDate
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.LastStatusDate)
                .NotEmpty()
                .WithMessage("LastStatusDate is required.")
                .GreaterThanOrEqualTo(x => x.AddNewLocalDrivingLicenseDTO.ApplicationDate)
                .WithMessage("LastStatusDate cannot be earlier than ApplicationDate.");

            // Validate PaidFees
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.PaidFees)
                .GreaterThanOrEqualTo(0)
                .WithMessage("PaidFees must be greater than or equal to 0.");

            // Validate CreatedByUserId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.CreatedByUserId)
                .GreaterThan(0)
                .WithMessage("CreatedByUserId must be greater than 0.");
        }
    }


}
