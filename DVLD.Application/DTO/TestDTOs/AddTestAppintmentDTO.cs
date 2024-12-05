namespace DVLD.Application.DTO.TestDTOs
{
    public class AddTestAppintmentDTO
    {
        public int TestTypeId { get; set; }
        public int LocalDrivingLicenseApplicationId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
