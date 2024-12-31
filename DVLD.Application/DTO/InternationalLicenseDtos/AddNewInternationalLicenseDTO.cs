namespace DVLD.Application.DTO.InternationalLicenseDtos
{
    public class AddNewInternationalLicenseDTO
    {
        public required int DriverId { get; set; }
        public required int IssuedUsingLocalLicenseId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
