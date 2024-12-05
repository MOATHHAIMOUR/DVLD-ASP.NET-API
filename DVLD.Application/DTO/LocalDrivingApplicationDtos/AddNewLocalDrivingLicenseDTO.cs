using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.LocalDrivingApplicationDtos
{
    public class AddNewLocalDrivingLicenseDTO
    {
        public int ApplicantPersonId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public EnumApplicationType ApplicationTypeId { get; set; }
        public EnumApplicationStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserId { get; set; }
        public EnumLicenseClass LicenseClassId { get; set; }
    }
}
