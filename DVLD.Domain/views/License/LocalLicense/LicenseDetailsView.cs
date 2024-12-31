using DVLD.Domain.Enums;

namespace DVLD.Domain.views.License.LocalLicense
{
    public class LicenseDetailsView
    {
        public required EnumLicenseClass LicenseClassId { get; set; }
        public required int LocalDrivingLicenseApplicationId { get; set; }
        public required string ClassName { get; set; } = string.Empty;
        public required string FullName { get; set; } = string.Empty;
        public required int LicenseId { get; set; }
        public required string NationalNo { get; set; } = string.Empty;
        public required EnumGender Gender { get; set; }
        public required DateTime IssueDate { get; set; }
        public required int IssueReason { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public required bool IsActive { get; set; }

        public decimal? FineFees { get; set; }

        public required DateTime DateOfBirth { get; set; }
        public required int DriverId { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required bool IsDetain { get; set; }
        public required string ImagePath { get; set; }

    }
}
