namespace DVLD.Domain.Entites
{
    public class ApplicationType
    {
        public required int ApplicationTypeID { get; set; }
        public required string ApplicationTypeTitle { get; set; }
        public required decimal ApplicationFees { get; set; }
    }
}
