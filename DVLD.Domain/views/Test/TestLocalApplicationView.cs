using DVLD.Domain.Enums;

namespace DVLD.Domain.views.Test
{
    public class TestLocalApplicationView
    {
        public int LocalDrivingLicenseApplicationId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int ApplicationId { get; set; }
        public EnumApplicationStatus ApplicationStatus { get; set; }
        public int PassedTests { get; set; }
        public decimal PaidFees { get; set; }
        public string ApplicationTypeTitle { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public string Username { get; set; } = string.Empty;
        public int PersonId { get; set; }
    }
}
