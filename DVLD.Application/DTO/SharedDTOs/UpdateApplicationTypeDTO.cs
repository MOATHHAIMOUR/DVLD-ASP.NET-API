namespace DVLD.Application.DTO.SharedDTOs
{
    public class UpdateApplicationTypeDTO
    {
        public required int ApplicationTypeID { get; set; }
        public required string ApplicationTypeTitle { get; set; }
        public required int ApplicationFees { get; set; }
    }
}
