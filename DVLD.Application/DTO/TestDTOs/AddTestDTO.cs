namespace DVLD.Application.DTO.TestDTOs
{
    public class AddTestDTO
    {
        public int TestAppointmentId { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int CreatedByUserId { get; set; }
    }
}
