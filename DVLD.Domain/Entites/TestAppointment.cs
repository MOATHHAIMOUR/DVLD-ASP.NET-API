namespace DVLD.Domain.Entites
{
    public class TestAppointment
    {
        public int TestAppointmentId { get; set; }
        public int TestTypeId { get; set; }
        public int LocalDrivingLicenseApplicationId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationId { get; set; }
    }
}
