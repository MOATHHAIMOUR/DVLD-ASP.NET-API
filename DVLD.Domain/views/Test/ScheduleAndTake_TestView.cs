namespace DVLD.Domain.views.Test
{
    public class ScheduleAndTake_TestView
    {
        public required int LocalDrivingLicenseApplicationId { get; set; }
        public required int TestAppointmentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string ClassName { get; set; }
        public required decimal TestTypeFees { get; set; }
        public required int Tries { get; set; }
    }
}
