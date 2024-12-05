using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.LocalDrivingApplicationDtos
{
    public class SearchLocalDrivingApplicationViewDto
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string NationalNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public int PassedTests { get; set; }
        public EnumApplicationStatus ApplicationStatus { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; } = "ASC";
    }
}
