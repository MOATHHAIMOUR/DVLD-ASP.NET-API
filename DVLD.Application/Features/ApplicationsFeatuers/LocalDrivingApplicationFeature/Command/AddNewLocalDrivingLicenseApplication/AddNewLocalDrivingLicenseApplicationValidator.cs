namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicenseApplication
{
    using FluentValidation;

    public class AddNewLocalDrivingLicenseApplicationValidator : AbstractValidator<AddNewLocalDrivingLicenseApplicationCommandHandler>
    {
        public AddNewLocalDrivingLicenseApplicationValidator()
        {
            // Validate ApplicantPersonId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.ApplicantPersonId)
                .GreaterThan(0)
                .WithMessage("ApplicantPersonId must be greater than 0.");



            // Validate ApplicationTypeId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.ApplicationTypeId)
                .IsInEnum()
                .WithMessage("ApplicationTypeId must be a valid value from EnumApplicationType.");

            // Validate LicenseClassId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.LicenseClassId)
                .IsInEnum()
                .WithMessage("LicenseClassId must be a valid value from EnumLicenseClass.");


            // Validate CreatedByUserId
            RuleFor(x => x.AddNewLocalDrivingLicenseDTO.CreatedByUserId)
                .GreaterThan(0)
                .WithMessage("CreatedByUserId must be greater than 0.");
        }
    }


}
