namespace DVLD.Application.DTO.InternationalLicenseDtos
{
    public class AddNewInternationalLicenseDTO
    {
        public int DriverId { get; set; }
        public int IssuedUsingLocalLicenseId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
