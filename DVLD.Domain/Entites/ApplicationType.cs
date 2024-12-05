namespace DVLD.Domain.Entites
{
    public class ApplicationType
    {
        public required int ApplicationTypeId { get; set; }
        public required string ApplicationTypeTitle { get; set; }
        public required decimal ApplicationFees { get; set; }
    }
}
