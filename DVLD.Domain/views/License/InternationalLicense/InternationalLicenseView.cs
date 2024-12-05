using DVLD.Domain.Enums;

namespace DVLD.Domain.views.License.InternationalLicense
{
    public class InternationalLicenseView
    {
        public string FullName { get; set; } = string.Empty;
        public int InternationalLicenseId { get; set; }
        public string NationalNo { get; set; } = string.Empty;
        public EnumGender Gender { get; set; }
        public DateTime IssueDate { get; set; }
        public int ApplicationId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DriverId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
