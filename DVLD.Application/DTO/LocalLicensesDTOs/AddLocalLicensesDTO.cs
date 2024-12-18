namespace DVLD.Application.DTO.LocalLicensesDTOs
{
    public class AddLocalLicensesDTO
    {
        public required int ApplicationId { get; set; }
        public required int LicenseClassId { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
    }
}
