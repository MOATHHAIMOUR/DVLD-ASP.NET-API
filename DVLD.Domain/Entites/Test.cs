namespace DVLD.Domain.Entites
{
    public class Test
    {
        public int TestId { get; set; }
        public int TestAppointmentId { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int CreatedByUserId { get; set; }
    }

}
