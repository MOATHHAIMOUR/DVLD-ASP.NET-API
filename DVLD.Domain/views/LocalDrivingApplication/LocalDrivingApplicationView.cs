using DVLD.Domain.Enums;

namespace DVLD.Domain.views.LocalDrivingApplication
{
    public class LocalDrivingApplicationView
    {
        public int? LocalDrivingLicenseApplicationId { get; set; }
        public string? ClassName { get; set; } = string.Empty;
        public string? NationalNo { get; set; } = string.Empty;
        public string? FullName { get; set; } = string.Empty;
        public DateTime? ApplicationDate { get; set; }
        public int? PassedTests { get; set; }
        public EnumApplicationStatus? ApplicationStatus { get; set; }
    }
}
