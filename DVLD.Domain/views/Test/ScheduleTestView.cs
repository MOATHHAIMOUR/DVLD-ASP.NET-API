namespace DVLD.Domain.views.Test
{
    public class ScheduleTestView
    {
        public int LocalDrivingLicenseApplicationId { get; set; }

        public int TestAppointmentId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public decimal TestTypeFees { get; set; }
        public int Tries { get; set; }
    }
}
