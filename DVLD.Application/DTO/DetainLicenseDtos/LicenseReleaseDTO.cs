namespace DVLD.Application.DTO.DetainLicenseDtos
{
    public class LicenseReleaseDTO
    {
        public int LicenseId { get; set; }
        public DateTime ReleasedDate { get; set; }
        public int ReleasedByUserId { get; set; }
    }
}
