using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.LocalDrivingApplicationDtos
{
    public class GetLocalDrivingApplicationInfoDTO
    {
        public int LocalDrivingLicenseApplicationId { get; set; }
        public string className { get; set; }
        public int ApplicationId { get; set; }
        public EnumApplicationStatus ApplicationStatus { get; set; }
        public int PassedTest { set; get; }
        public decimal ApplicationFees { get; set; }
        public EnumApplicationType ApplicationTypeId { get; set; }
        public int PersonId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime LastStatusDate { get; set; }
        public DateTime StatusDate { get; set; }
        int UserId { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;

    }
}
