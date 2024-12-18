namespace DVLD.Domain.DomainSearchParameters
{
    public class LocalDrivingApplicationsSearchParameters
    {
        public int? LocalDrivingLicenseApplicationID { get; set; }
        public string? ClassName { get; set; }
        public string? NationalNo { get; set; }
        public string? FullName { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public int? PassedTests { get; set; }
        public string? ApplicationStatus { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; } = "ASC";
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
