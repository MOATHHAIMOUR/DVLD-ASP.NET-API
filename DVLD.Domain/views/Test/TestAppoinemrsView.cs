namespace DVLD.Domain.views.Test
{
    public class TestAppointmentView
    {
        public int LocalDrivingLicenseApplicationId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TestTypeTitle { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string TestResult { get; set; } = string.Empty;
        public string IsLocked { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string AppointmentCreatedByUser { get; set; } = string.Empty;
    }
}
