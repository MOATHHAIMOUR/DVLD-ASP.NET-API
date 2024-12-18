using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.LocalDrivingApplicationDtos
{
    public class AddNewLocalDrivingLicenseApplicationDTO
    {
        public int ApplicantPersonId { get; set; }
        public EnumApplicationType ApplicationTypeId { get; set; }
        public EnumLicenseClass LicenseClassId { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
