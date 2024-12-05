using DVLD.Domain.Enums;

namespace DVLD.Domain.views.License.LocalLicense
{
    public class LicenseDetailsView
    {
        public EnumLicenseClass LicenseClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int LicenseId { get; set; }
        public string NationalNo { get; set; } = string.Empty;
        public EnumGender Gender { get; set; }
        public DateTime IssueDate { get; set; }
        public int? IssueReason { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DriverId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsDetain { get; set; }

    }
}
