using DVLD.Domain.Enums;

namespace DVLD.Domain.views.LocalDrivingApplication
{
    public class LocalDrivingApplicationView
    {
        public required int LocalDrivingLicenseApplicationId { get; set; }
        public required string ClassName { get; set; } = string.Empty;
        public required string NationalNo { get; set; } = string.Empty;
        public required string FullName { get; set; } = string.Empty;
        public required DateTime ApplicationDate { get; set; }
        public required int PassedTests { get; set; }
        public required EnumApplicationStatus ApplicationStatus { get; set; }
    }
}
