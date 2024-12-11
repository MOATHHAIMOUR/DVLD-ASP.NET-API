namespace DVLD.Application.DTO.DetainLicenseDtos
{
    public class AddNewDetainLicenseDTO
    {
        public int LicenseId { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
