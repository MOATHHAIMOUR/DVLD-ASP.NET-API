namespace DVLD.Application.DTO.LocalDrivingApplicationDtos
{
    public class DetainLicenseDTO
    {
        public int LicenseId { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
