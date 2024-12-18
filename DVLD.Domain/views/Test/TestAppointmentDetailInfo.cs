namespace DVLD.Domain.views.Test
{
    public class TestAppointmentDetailInfo
    {
        public required int LocalDrivingLicenseApplicationId { get; set; }
        public required int PassedTests { get; set; }
        public required string ClassName { get; set; }
        public required int ApplicationId { get; set; }
        public required string ApplicationStatus { get; set; }
        public required decimal PaidFees { get; set; }
        public required string ApplicationTypeTitle { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime ApplicationDate { get; set; }
        public required DateTime LastStatusDate { get; set; }
        public required string Username { get; set; }
        public required int PersonId { get; set; }

    }

}
