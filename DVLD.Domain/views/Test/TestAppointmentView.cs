namespace DVLD.Domain.views.Test
{
    public class TestAppointmentView
    {
        public required int TestAppointmentId { get; set; }
        public required DateTime AppointmentDate { get; set; }
        public required decimal PaidFees { get; set; }
        public required bool IsLocked { get; set; }
        public bool? TestResult { get; set; }
    }
}
